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
            CreateMap<User, StudentModel>();

            //.ReverseMap()
            CreateMap<StudentModel, User>()
                .ForMember(u => u.Password, cfg => cfg.Ignore());

            // Mapping StudentInfoModel and User (also reverse)
            CreateMap<User, StudentInfoModel>();
            CreateMap<StudentInfoModel, User>();
        }
    }
}
