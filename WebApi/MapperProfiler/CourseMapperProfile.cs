using AutoMapper;
using BLL.Models.Course;
using DAL.Entities;

namespace WebApi.MapperProfiler
{
    public class CourseMapperProfile: Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, CourseModel>();
            CreateMap<CourseModel, Course>();
        }
    }
}
