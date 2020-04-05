using AutoMapper;
using BLL.Models.StudentProfile;
using DAL.Entities;

namespace WebApi.MapperProfiler
{
    class StudentMapperProfile : Profile
    {
        public StudentMapperProfile()
        {
            // Mapping StudentModel and User (aslo reverse)
            CreateMap<User, UserModel>();

            //.ReverseMap()
            CreateMap<UserModel, User>()
                .ForMember(u => u.Password, cfg => cfg.Ignore());

            // Mapping StudentInfoModel and User (also reverse)
            CreateMap<User, StudentInfoModel>()
                .ForMember(u => u.Courses, uc => uc.MapFrom(c => c.UserCourses));
            CreateMap<StudentInfoModel, User>();
        }
    }
}
