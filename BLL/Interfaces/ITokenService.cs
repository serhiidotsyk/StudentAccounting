using BLL.Models.StudentProfile;
using BLL.Models.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        public Task<JwtSecurityToken> GenerateAccessTokenAsync(int userId);
        public Task<RefreshTokenModel> GenerateRefreshTokenAsync(UserModel userModel);
        public Task<RefreshTokenModel> ValidateRefreshTokenAsync(string token);
    }
}
