using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using DAL;
using DAL.Entities;
using DAL.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        public AuthService(ApplicationDbContext context, IMapper mapper, IMailService mailService)
        {
            _context = context;
            _mapper = mapper;
            _mailService = mailService;
        }
        public async Task<(bool, UserModel)> SignInAsync(UserSignInModel userSignInModel)
        {
            var user = await _context.Users.Where(u => u.Email == userSignInModel.Email).SingleOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException();
            }
            if (user.Password.Equals(userSignInModel.Password) && user.IsEmailComfirmed == true)
            {
                return (true, _mapper.Map<UserModel>(user));
            }
            else
            {
                return (false, null);
            }

        }

        public async Task<(bool, UserModel)> SocialLoginAsync(UserSocialLogin userSocialLogin)
        {
            var user = await _context.Users.Where(u => u.Email == userSocialLogin.Email).SingleOrDefaultAsync();

            if (user == null)
            {
                return (true, await SocialSignUpAsync(userSocialLogin));
            }
            if (user.Email == userSocialLogin.Email)
            {
                return (true, _mapper.Map<UserModel>(user));
            }
            else
            {
                return (false, null);
            }
        }

        private async Task<UserModel> SocialSignUpAsync(UserSocialLogin userSocialLogin)
        {
            var user = _mapper.Map<UserSocialLogin, User>(userSocialLogin, cfg =>
                cfg.AfterMap((src, dest) =>
                {
                    dest.RegisteredDate = DateTime.Now;
                    dest.Age = new DateTime((DateTime.UtcNow - Convert.ToDateTime(src.BirthDate)).Ticks).Year;
                    dest.RoleId = (int)RoleType.Student;
                }));

            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserModel>(user);
            }
            return null;
        }

        public async Task<UserModel> SignUpAsync(UserSignUpModel userSignUpModel)
        {
            var isUserExists = await _context.Users.Where(u => u.Email == userSignUpModel.Email).SingleOrDefaultAsync();
            if (isUserExists != null)
                return null;

            var user = _mapper.Map<UserSignUpModel, User>(userSignUpModel, cfg =>
                cfg.AfterMap((src, dest) =>
                {
                    dest.RegisteredDate = DateTime.Now;
                    dest.RoleId = (int)RoleType.Student;
                }));

            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userValidation = _mapper.Map<UserModel>(user);

                string callBackUrl = await _mailService.GenerateConfirmationLinkAsync(userValidation);

                await _mailService.SendConfirmationLinkAsync(userValidation.Email,
                    $"Confirm registration following the link: <a href='{callBackUrl}'>Confirm email NOW</a>");

                return userValidation;
            }

            return null;
        }

        public async Task<UserModel> ConfirmEmailAsync(int userId, string token)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                if (user.EmailConfirmationToken.Equals(token))
                {
                    user.IsEmailComfirmed = true;

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                return _mapper.Map<UserModel>(user);
            }
            return null;

        }
    }
}
