using BLL.Models.StudentProfile;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        public StudentModel GetStudent(int studentId);
        public ICollection<StudentInfoModel> GetAllStudents();
        public StudentModel EditSudent(StudentModel studentModel, int id);
        public StudentModel Delete(int id);
        public ICollection<StudentModel> Delete(int[] ids);
    }
}
