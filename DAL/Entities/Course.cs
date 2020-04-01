using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Course
    {
        /// <summary>
        /// Gets or sets course id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets course name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// gets or sets course duration in days
        /// </summary>
        public int DurationDays { get; set; }

        /// <summary>
        /// Navigation property to user courses
        /// </summary>
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
