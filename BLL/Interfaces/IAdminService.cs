using BLL.Models.StudentProfile;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        public StudentInfoModel GetStudent(int studentId);
        public ICollection<StudentInfoModel> GetAllStudents();
        public StudentModel UpdateStudent(StudentModel studentModel, int studentId);
        public StudentModel Delete(int studentId);
        public ICollection<StudentModel> Delete(int[] studentIds);
    }
}
