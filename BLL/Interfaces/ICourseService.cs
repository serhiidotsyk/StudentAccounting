using BLL.Models.Course;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        public CourseModel GetCourse(int id);
        public ICollection<CourseModel> GetAllCourses();
        public ICollection<CourseModel> GetCoursesByStudentId(int studentId);
        public CourseModel CreateCourse(CourseModel createCourseModel);
        public CourseModel SubscribeToCourse(SubscribeToCourseModel subscribeToCourseModel);
        public CourseModel UpdateCourse(CourseModel courseModel,int courseId);
        public CourseModel DeleteCourse(int id);
        public ICollection<CourseModel> DeleteCourses(int[] ids);
    }
}
