using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Password { get; set; }
        public DateTime? StudyStart { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
