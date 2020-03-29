using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        public CourseModel GetCourse(int id);
        public ICollection<CourseModel> GetAllCourses();
        public ICollection<CourseModel> GetAvailableCourses(int userId);
        public ICollection<CourseInfoModel> GetCoursesByStudentId(int studentId);
        public CourseModel CreateCourse(CourseModel createCourseModel);
        public CourseModel SubscribeToCourse(SubscribeToCourseModel subscribeToCourseModel);
        public UserCourseModel UnSubscribeFromCourse(UserCourseModel userCourseModel);
        public CourseModel UpdateCourse(CourseModel courseModel,int courseId);
        public CourseModel DeleteCourse(int id);
        public ICollection<CourseModel> DeleteCourses(int[] ids);
    }
}
