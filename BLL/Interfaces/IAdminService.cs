using BLL.Helpers.Pagination;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        public StudentInfoModel GetStudent(int studentId);
        public (ICollection<StudentInfoModel>, int) GetAllStudents(QueryStringParams queryStringParams);
        public UserModel CreateStudent(UserSignUpModel studentModel);
        public UserModel UpdateStudent(UserModel studentModel);
        public UserModel Delete(int studentId);
        public ICollection<UserModel> Delete(int[] studentIds);
    }
}
