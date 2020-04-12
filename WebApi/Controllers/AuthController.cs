using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models.Auth;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserSignUpModel userSignUpModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { title = "Model is not valid" });
            var user = await _authService.SignUpAsync(userSignUpModel);
            if (user != null)
            {
                return Ok(new
                {
                    title = "User was successfully registered"
                });
            }

            return BadRequest(new { title = "User was not registered" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(int userId, string token)
        {
            var user = await _authService.ConfirmEmailAsync(userId, token);
            if (user != null)
                return Ok("Email Confirmed!");
            else
                return BadRequest("User error");
        }

        [HttpPost("signIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserSignInModel userSignInModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { title = "Model is not valid" });
            var (state ,user) = await _authService.SignInAsync(userSignInModel);            
            if(state)
            {
                var access_token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GenerateAccessTokenAsync(user.Id));
                var refresh_token = (await _tokenService.GenerateRefreshTokenAsync(user)).Token;
                    
                return Ok(new
                {
                    access_token,
                    refresh_token
                });
            }

            return NotFound(new { title = "You have entered an invalid username or password" });
        }
        [HttpPost("socialLogin")]
        public async Task<IActionResult> SocialLogin(UserSocialLogin userSocialLogin)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (state, user) = await _authService.SocialLoginAsync(userSocialLogin);
            if (state)
            {
                var access_token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GenerateAccessTokenAsync(user.Id));
                var refresh_token = (await _tokenService.GenerateRefreshTokenAsync(user)).Token;

                return Ok(new
                {
                    access_token,
                    refresh_token
                });
            }
            return NotFound(new { title = "Couldnt login via exernal provider" });
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshTokenModel)
        {
            var refreshToken = await _tokenService.ValidateRefreshTokenAsync(refreshTokenModel);
            if (refreshToken == null)
            {
                return BadRequest(new { title = "invalid_grant" });
            }
            var access_token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GenerateAccessTokenAsync(refreshToken.UserId));
            var refresh_token = refreshToken.Token;

            return Ok(new
            {
                access_token,
                refresh_token
            });
        }
    }
}