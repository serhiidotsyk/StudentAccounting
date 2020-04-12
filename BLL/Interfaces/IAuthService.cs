using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Signs in user async
        /// </summary>
        /// <param name="userSignInModel"></param>
        /// <returns>
        /// Signed in user
        /// </returns>
        public Task<(bool, UserModel)> SignInAsync(UserSignInModel userSignInModel);

        /// <summary>
        /// Logins user by social provider or signs up if user not exist
        /// </summary>
        /// <param name="userSignInModel"></param>
        /// <returns>
        /// Logined user
        /// </returns>
        public Task<(bool, UserModel)> SocialLoginAsync(UserSocialLogin userSignInModel);

        /// <summary>
        /// Signs up user async
        /// </summary>
        /// <param name="userSignUpModel"></param>
        /// <returns>
        /// Signed up user
        /// </returns>
        public Task<UserModel> SignUpAsync(UserSignUpModel userSignUpModel);

        /// <summary>
        /// Confirms user email async
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>
        /// User whose email was confirmed
        /// </returns>
        public Task<UserModel> ConfirmEmailAsync(int userId, string token);
    }
}
