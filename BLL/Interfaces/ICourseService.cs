using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        public Task<CourseModel> GetCourseAsync(int id);
        public Task<ICollection<CourseModel>> GetAllCoursesAsync();
        public Task<ICollection<CourseModel>> GetAvailableCoursesAsync(int userId);
        public Task<ICollection<CourseInfoModel>> GetCoursesByStudentIdAsync(int studentId);
        public Task<CourseModel> CreateCourseAsync(CourseModel createCourseModel);
        public Task<CourseModel> SubscribeToCourseAsync(SubscribeToCourseModel subscribeToCourseModel);
        public Task<UserCourseModel> UnSubscribeFromCourseAsync(UserCourseModel userCourseModel);
        public Task<CourseModel> UpdateCourseAsync(CourseModel courseModel,int courseId);
        public Task<CourseModel> DeleteCourseAsync(int id);
        public Task<ICollection<CourseModel>> DeleteCoursesAsync(int[] ids);
    }
}
