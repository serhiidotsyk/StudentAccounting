using BLL.Helpers.FluentValidation.Auth;
using BLL.Models.StudentProfile;
using FluentValidation;

namespace BLL.Helpers.FluentValidation.User
{
    public class UserValidation : AbstractValidator<UserModel>
    {
        public UserValidation()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.Age).NotEmpty().GreaterThan((int)AgeRestrictions.MinAge).LessThan((int)AgeRestrictions.MaxAge);
        }
    }
}
