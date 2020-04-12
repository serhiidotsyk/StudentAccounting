using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<(bool, UserModel)> SignInAsync(UserSignInModel userSignInModel);
        public Task<(bool, UserModel)> SocialLoginAsync(UserSocialLogin userSignInModel);
        public Task<UserModel> SignUpAsync(UserSignUpModel userSignUpModel);
        public Task<UserModel> ConfirmEmailAsync(int userId, string token);
    }
}
