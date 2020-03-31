using System;

namespace DAL.Entities
{
    public class UserCourse
    {
        public int StudentId { get; set; }
        public User Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
