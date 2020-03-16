using BLL.Models.StudentProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IStudentProfileService
    {
        public UserModel GetStudentProfile(int id);
        public UserModel EditStudentProfile(int id);
    }
}
