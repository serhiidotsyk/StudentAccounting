using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.UserCourseModel
{
    public class UserCourseModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
