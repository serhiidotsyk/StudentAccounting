using BLL.Models.Auth;
using FluentValidation;

namespace BLL.Helpers.FluentValidation.Auth
{
    public class UserSocialLoginValidation : AbstractValidator<UserSocialLogin>
    {
        public UserSocialLoginValidation()
        {
            RuleFor(n => n.Name).NotEmpty();
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(b => b.BirthDate).NotEmpty();
        }
    }
}
