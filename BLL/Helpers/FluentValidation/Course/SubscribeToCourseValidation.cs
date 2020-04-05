using BLL.Models.Course;
using FluentValidation;
using System;

namespace BLL.Helpers.FluentValidation.Course
{
    public class SubscribeToCourseValidation : AbstractValidator<SubscribeToCourseModel>
    {
        public SubscribeToCourseValidation()
        {
            RuleFor(c => c.CourseId).NotEmpty();
            RuleFor(c => c.StudentId).NotEmpty();
            RuleFor(c => c.EnrollmentDate).GreaterThan(new DateTimeOffset(DateTime.UtcNow)
                                           .ToUnixTimeMilliseconds())
                                            .WithMessage("Enrollment date can not be set to this date");
        }
    }
}
