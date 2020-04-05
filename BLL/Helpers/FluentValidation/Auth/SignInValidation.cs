using BLL.Models.Auth;
using FluentValidation;

namespace BLL.Helpers.FluentValidation.Auth
{
    public class SignInValidation : AbstractValidator<UserSignInModel>
    {
        public SignInValidation()
        {
            RuleFor(s => s.Email).EmailAddress().NotEmpty();
            RuleFor(s => s.Password).NotEmpty();
        }
    }
}
