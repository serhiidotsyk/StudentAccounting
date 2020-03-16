using AutoMapper;
using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using DAL.Entities;

namespace WebApi.MapperProfiler
{
    public class UserCourseProfile: Profile
    {
        public UserCourseProfile()
        {
            CreateMap<UserCourseModel, UserCourse>();
            CreateMap<UserCourse, UserCourseModel>();

            CreateMap<SubscribeToCourseModel, UserCourse>();
            CreateMap<UserCourse, SubscribeToCourseModel>();
        }
    }
}
