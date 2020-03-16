using BLL.Models.StudentProfile;
using BLL.Models.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(int userId);
        public RefreshTokenModel GenerateRefreshToken(UserModel userModel);
        public RefreshTokenModel ValidateRefreshToken(string token);
    }
}
