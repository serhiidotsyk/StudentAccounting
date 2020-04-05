using BLL.Models.UserCourseModel;
using FluentValidation;

namespace BLL.Helpers.FluentValidation.Course
{
    public class UserCourseValidation : AbstractValidator<UserCourseModel>
    {
        public UserCourseValidation()
        {
            RuleFor(uc => uc.CourseId).NotEmpty();
            RuleFor(uc => uc.StudentId).NotEmpty();
        }
    }
}
