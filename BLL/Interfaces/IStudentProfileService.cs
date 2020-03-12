using BLL.Models.StudentProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IStudentProfileService
    {
        public StudentModel GetStudentProfile(int id);
        public StudentModel EditStudentProfile(int id);
    }
}
