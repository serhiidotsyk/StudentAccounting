using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
