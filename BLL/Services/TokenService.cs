using BLL.Interfaces;
using BLL.Models.StudentProfile;
using BLL.Models.Token;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using BLL.Models.AppSettings;
using DAL;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private const int MAXIMUM_LOGGED_DEVICES = 5;
        private const int REFRESH_TOKEN_LIFETIME = 15;

        public TokenService(ApplicationDbContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<JwtSecurityToken> GenerateAccessTokenAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Role)
                                            .Where(i => i.Id == userId)
                                             .SingleOrDefaultAsync();

            var claims = new List<Claim>
            {
                new Claim("id", userId.ToString()),
                new Claim("email", user.Email),
                new Claim("role", user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var currentTime = DateTime.UtcNow;
            
            var token = new JwtSecurityToken(
                issuer: _appSettings.JwtIssuer,
                notBefore: currentTime,
                claims: claims,
                expires: currentTime.AddMinutes(_appSettings.JwtExpiresMinutes),
                signingCredentials: credential);

            return token;
        }

        public async Task<RefreshTokenModel> GenerateRefreshTokenAsync(UserModel userModel)
        {
            ICollection<RefreshToken> tokens = await _context.Tokens.Where(t => t.UserId == userModel.Id)
                                                                     .ToListAsync();

            //you have to relogin if number of logged devices higher than maximum allowed
            if (tokens.Count() > MAXIMUM_LOGGED_DEVICES)
            {
                foreach (var token in tokens)
                {
                    _context.Tokens.Remove(token);
                }
                await _context.SaveChangesAsync();
            }
            var newRefreshToken = new RefreshToken
            {
                UserId = userModel.Id,
                Token = Guid.NewGuid().ToString(),
                ExpirationDate= DateTime.UtcNow.AddDays(REFRESH_TOKEN_LIFETIME),
                Revoked = false
            };

            _context.Tokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<RefreshTokenModel>(newRefreshToken);
        }

        public async Task<RefreshTokenModel> ValidateRefreshTokenAsync(string token)
        {
            var refreshedToken = await _context.Tokens.SingleOrDefaultAsync(x => x.Token == token);
            if (refreshedToken == null)
                return null;

            if (refreshedToken.ExpirationDate < DateTime.UtcNow || refreshedToken.Revoked == true)
            {
                _context.Tokens.Remove(refreshedToken);
                await _context.SaveChangesAsync();
                return null;
            }

            string newRefreshToken = Guid.NewGuid().ToString();
            refreshedToken.Token = newRefreshToken;

            _context.Tokens.Update(refreshedToken);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<RefreshTokenModel>(refreshedToken);
        }
    }
}
