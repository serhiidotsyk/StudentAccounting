using AutoMapper;
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
        }
    }
}
