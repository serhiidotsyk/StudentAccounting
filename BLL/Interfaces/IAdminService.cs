using BLL.Helpers.Pagination;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdminService
    {
        /// <summary>
        /// Gets student async.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        /// Student info model.
        /// </returns>
        public Task<StudentInfoModel> GetStudentAsync(int studentId);

        /// <summary>
        /// Gets all students async.
        /// </summary>
        /// <param name="queryStringParams"></param>
        /// <returns>
        /// Filtered collection of students by <c>QueryStringParams</c>.
        /// </returns>
        public Task<(ICollection<StudentInfoModel>, int)> GetAllStudentsAsync(QueryStringParams queryStringParams);

        /// <summary>
        /// Creates student async.
        /// </summary>
        /// <param name="studentModel"></param>
        /// <returns>
        /// Student that was created.
        /// </returns>
        public Task<UserModel> CreateStudentAsync(UserSignUpModel studentModel);

        /// <summary>
        /// Updated student async
        /// </summary>
        /// <param name="studentModel"></param>
        /// <returns>
        /// Updated student
        /// </returns>
        public Task<UserModel> UpdateStudentAsync(UserModel studentModel);

        /// <summary>
        /// Deletes student async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        /// Deleted student model
        /// </returns>
        public Task<UserModel> DeleteAsync(int studentId);

        /// <summary>
        /// Deletes students async
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        /// Collection of deleted students 
        /// </returns>
        public Task<ICollection<UserModel>> DeleteAsync(int[] studentIds);
    }
}
