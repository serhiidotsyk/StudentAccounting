using BLL.Models.StudentProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    interface IUserProfile
    {
        public StudentProfileModel GetStudentProfile(int id);
        public StudentEditProfileModel EditStudentProfile(int id);
    }
}
