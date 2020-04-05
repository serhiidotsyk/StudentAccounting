using BLL.Models.Auth;
using BLL.Models.StudentProfile;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public (bool, UserModel) SignIn(UserSignInModel userSignInModel);
        public (bool, UserModel) SocialLogin(UserSocialLogin userSignInModel);
        public UserModel SignUp(UserSignUpModel userSignUpModel);
        public UserModel ConfirmEmail(int userId, string token);
    }
}
