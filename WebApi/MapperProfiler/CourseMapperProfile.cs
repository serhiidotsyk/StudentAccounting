using AutoMapper;
using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using DAL.Entities;

namespace WebApi.MapperProfiler
{
    public class CourseMapperProfile: Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, CourseModel>();
            CreateMap<CourseModel, Course>();

            CreateMap<Course, CourseInfoModel>();
            CreateMap<CourseInfoModel, Course>();

            CreateMap<Course, UserCourseModel>();
            CreateMap<UserCourseModel, Course>();
        }
    }
}
