using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        /// <summary>
        /// Gets course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Course model
        /// </returns>
        public Task<CourseModel> GetCourseAsync(int id);

        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <returns>
        /// Collection of all courses
        /// </returns>
        public Task<ICollection<CourseModel>> GetAllCoursesAsync();

        /// <summary>
        /// Gets avalilable courses for specific student
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Collection of available courses
        /// </returns>
        public Task<ICollection<CourseModel>> GetAvailableCoursesAsync(int studentId);

        /// <summary>
        /// Gets courses by student id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        /// Collection of courses by student
        /// </returns>
        public Task<ICollection<CourseInfoModel>> GetCoursesByStudentIdAsync(int studentId);

        /// <summary>
        /// Creates a course
        /// </summary>
        /// <param name="createCourseModel"></param>
        /// <returns>
        /// Created course model
        /// </returns>
        public Task<CourseModel> CreateCourseAsync(CourseModel createCourseModel);

        /// <summary>
        /// Subscribes a student to a course
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns>
        /// Course to which user was subscribed
        /// </returns>
        public Task<CourseModel> SubscribeToCourseAsync(SubscribeToCourseModel subscribeToCourseModel);

        /// <summary>
        /// Unsubscribes student from course
        /// </summary>
        /// <param name="userCourseModel"></param>
        /// <returns>
        /// Unsubscribed course model
        /// </returns>
        public Task<UserCourseModel> UnSubscribeFromCourseAsync(UserCourseModel userCourseModel);

        /// <summary>
        /// Updates course
        /// </summary>
        /// <param name="courseModel"></param>
        /// <param name="courseId"></param>
        /// <returns>
        /// Updated course model
        /// </returns>
        public Task<CourseModel> UpdateCourseAsync(CourseModel courseModel,int courseId);

        /// <summary>
        /// Deletes a course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Deleted course model
        /// </returns>
        public Task<CourseModel> DeleteCourseAsync(int id);

        /// <summary>
        /// Deletes a collection of courses
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>
        /// Collection of deleted courses
        /// </returns>
        public Task<ICollection<CourseModel>> DeleteCoursesAsync(int[] ids);
    }
}
