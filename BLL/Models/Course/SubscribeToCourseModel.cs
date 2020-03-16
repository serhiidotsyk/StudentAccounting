using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Course
{
    public class SubscribeToCourseModel
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
