using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models.Auth;
using BLL.Models.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserSignUpModel userSignUpModel)
        {
            var user = _authService.SignUp(userSignUpModel);
            if (user != null)
            {
                return Ok(new { message = "User successfully registered" });
            }

            return BadRequest(new { message = "User was not registered" });
        }
        
        [HttpPost("signIn")]
        public IActionResult SignIn(UserSignInModel userSignInModel)
        {
            var (state ,user) = _authService.SignIn(userSignInModel);            
            if(state)
            {
               var access_token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateAccessToken(user.Id));
                var refresh_token = _tokenService.GenerateRefreshToken(user).Token;

                return Ok(new
                {
                    access_token,
                    refresh_token
                });
            }

            return NotFound(new { message = "You have entered an invalid username or password" });
        }

        [HttpPost("refreshToken")]
        public IActionResult RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            var refreshedToken = _tokenService.ValidateRefreshToken(refreshTokenModel.Token);
            if (refreshedToken == null)
            {
                return BadRequest(new { message = "invalid_grant" });
            }

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateAccessToken(refreshTokenModel.UserId)),
                refresh_token = refreshedToken.Token
            });
        }
    }
}