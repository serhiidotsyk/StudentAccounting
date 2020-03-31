using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets user email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user email confirmed
        /// </summary>
        public bool IsEmailComfirmed { get; set; }

        /// <summary>
        /// Get or sets email confirmation token
        /// </summary>
        public string EmailConfirmationToken { get; set; }

        /// <summary>
        /// Gets or sets user registered date
        /// </summary>
        public DateTime RegisteredDate { get; set; }

        /// <summary>
        /// Gets or sets user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets user study start date
        /// </summary>
        public DateTime? StudyStart { get; set; }

        /// <summary>
        /// Navigation property to role
        /// </summary>
        public int RoleId { get; set; }
        public Role Role { get; set; }

        /// <summary>
        /// navigation property to refresh token
        /// </summary>
        public ICollection<RefreshToken> Tokens { get; set; }

        /// <summary>
        /// Navigation property to user courses
        /// </summary>
        public ICollection<UserCourse> UserCourses { get; set; }

        /// <summary>
        /// Navigation property to scheduld ids
        /// </summary>
        public ICollection<ScheduledJob> ScheduledJobs { get; set; }
    }
}
