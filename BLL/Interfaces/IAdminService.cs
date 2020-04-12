using BLL.Helpers.Pagination;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        public Task<StudentInfoModel> GetStudentAsync(int studentId);
        public Task<(ICollection<StudentInfoModel>, int)> GetAllStudentsAsync(QueryStringParams queryStringParams);
        public Task<UserModel> CreateStudentAsync(UserSignUpModel studentModel);
        public Task<UserModel> UpdateStudentAsync(UserModel studentModel);
        public Task<UserModel> DeleteAsync(int studentId);
        public Task<ICollection<UserModel>> DeleteAsync(int[] studentIds);
    }
}
