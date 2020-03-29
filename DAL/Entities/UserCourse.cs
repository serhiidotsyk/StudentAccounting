using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class UserCourse
    {
        public int StudentId { get; set; }
        public User Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
