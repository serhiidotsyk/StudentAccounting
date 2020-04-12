using BLL.Models.StudentProfile;
using BLL.Models.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates token for user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// JWT token
        /// </returns>
        public Task<JwtSecurityToken> GenerateAccessTokenAsync(int userId);

        /// <summary>
        /// Generates and saves refresh tokens for renewing access tokens in DB 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// Refresh token
        /// </returns>
        public Task<RefreshTokenModel> GenerateRefreshTokenAsync(UserModel userModel);

        /// <summary>
        /// Validates refresh token that was sent by user
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// Refresh token
        /// </returns>
        public Task<RefreshTokenModel> ValidateRefreshTokenAsync(string token);
    }
}
