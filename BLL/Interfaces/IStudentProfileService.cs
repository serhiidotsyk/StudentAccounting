using BLL.Models.StudentProfile;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStudentProfileService
    {
        /// <summary>
        /// Gets student profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Studend model
        /// </returns>
        public Task<UserModel> GetStudentProfile(int id);
    }
}
