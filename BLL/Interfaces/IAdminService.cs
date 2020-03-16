using BLL.Models.StudentProfile;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        public StudentInfoModel GetStudent(int studentId);
        public ICollection<StudentInfoModel> GetAllStudents();
        public UserModel UpdateStudent(UserModel studentModel, int studentId);
        public UserModel Delete(int studentId);
        public ICollection<UserModel> Delete(int[] studentIds);
    }
}
