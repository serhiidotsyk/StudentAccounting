using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Course
{
    public class SubscribeToCourseModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public long EnrollmentDate { get; set; }
    }
}
