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

            CreateMap<UserCourseModel, CourseModel>();
            CreateMap<CourseModel, UserCourseModel>();

            CreateMap<SubscribeToCourseModel, UserCourse>();
            CreateMap<UserCourse, SubscribeToCourseModel>();

            CreateMap<UserCourse, CourseInfoModel>()
                .ForMember(c => c.Id, uc => uc.MapFrom(s => s.CourseId))
                .ForMember(c => c.Name, uc => uc.MapFrom(s => s.Course.Name))
                .ForMember(c => c.DurationDays, uc => uc.MapFrom(s => s.Course.DurationDays));
            CreateMap<CourseInfoModel, UserCourse>();
        }
    }
}
