using System.IdentityModel.Tokens.Jwt;
using BLL.Interfaces;
using BLL.Models.Auth;
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
        public  IActionResult Register(UserSignUpModel userSignUpModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Model is not valid" });
            var user = _authService.SignUp(userSignUpModel);
            if (user != null)
            {
                return Ok(new
                {
                    message = "User was successfully registered"
                });
            }

            return BadRequest(new { message = "User was not registered" });
        }

        [HttpGet("ConfirmEmail")]
        public IActionResult ConfirmEmail(int userId, string token)
        {
            var user = _authService.ConfirmEmail(userId, token);
            if (user != null)
                return Ok("Email Confirmed!");
            else
                return BadRequest("User error");
        }

        [HttpPost("signIn")]
        public IActionResult SignIn(UserSignInModel userSignInModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Model is not valid" });
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
        [HttpPost("socialLogin")]
        public IActionResult SocialLogin(UserSocialLogin userSocialLogin)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (state, user) = _authService.SocialLogin(userSocialLogin);
            if (state)
            {
                var access_token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateAccessToken(user.Id));
                var refresh_token = _tokenService.GenerateRefreshToken(user).Token;

                return Ok(new
                {
                    access_token,
                    refresh_token
                });
            }
            return NotFound(new { message = "Couldnt login via exernal provider" });
        }

        [HttpPost("refreshToken")]
        public IActionResult RefreshToken(string refreshTokenModel)
        {
            var refreshedToken = _tokenService.ValidateRefreshToken(refreshTokenModel);
            if (refreshedToken == null)
            {
                return BadRequest(new { message = "invalid_grant" });
            }

            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateAccessToken(refreshedToken.UserId)),
                refresh_token = refreshedToken.Token
            });
        }
    }
}