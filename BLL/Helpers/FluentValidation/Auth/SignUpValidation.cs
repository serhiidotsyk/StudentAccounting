using BLL.Models.Auth;
using FluentValidation;

namespace BLL.Helpers.FluentValidation.Auth
{
    public enum AgeRestrictions { MinAge = 18, MaxAge = 70 };
    public class SignUpValidation : AbstractValidator<UserSignUpModel>
    {
        public SignUpValidation()
        {
            RuleFor(s => s.FirstName).NotEmpty();
            RuleFor(s => s.LastName).NotEmpty();
            RuleFor(s => s.Email).EmailAddress().NotEmpty();
            RuleFor(s => s.Age).NotEmpty()
                                .GreaterThan((int)AgeRestrictions.MinAge)
                                 .LessThan((int)AgeRestrictions.MaxAge)
                                  .WithMessage("You do not match our age restrictions");
            RuleFor(s => s.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
        }
    }
}
