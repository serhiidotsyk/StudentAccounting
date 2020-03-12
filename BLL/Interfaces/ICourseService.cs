using BLL.Models.Course;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        public CourseModel GetCourse(int id);
        public ICollection<CourseModel> GetAllCourses();
        public CourseModel CreateCourse(CourseModel createCourseModel);
        public CourseModel SubscribeToCourse(int userId, int courseId);
        public CourseModel UpdateCourse(CourseModel courseModel,int courseId);
        public CourseModel DeleteCourse(int id);
        public ICollection<CourseModel> DeleteCourses(int[] ids);
    }
}
