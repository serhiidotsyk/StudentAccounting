using BLL.Models.Course;
using System.Collections.Generic;

namespace BLL.Models.StudentProfile
{
    public class StudentInfoModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public ICollection<CourseInfoModel> Courses { get; set; }
    }
}
