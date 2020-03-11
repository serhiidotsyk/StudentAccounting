using AutoMapper;
using BLL.Models.StudentProfile;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.MapperProfiler
{
    class StudentMapperProfile : Profile
    {
        public StudentMapperProfile()
        {
            CreateMap<User, StudentProfileModel>();
            //.ForMember(u => u.FirstName, config => config.MapFrom(s=> s.FirstName))
            //.ForMember(u => u.LastName, config => config.MapFrom(s => s.LastName))
            //.ForMember(u => u.Age, config => config.MapFrom(s => s.Age))
            //.ReverseMap()
            //.ForMember(s => s.FirstName, config => config.MapFrom(u => u.FirstName))
            //.ForMember(s => s.LastName, config => config.MapFrom(u => u.LastName))
            //.ForMember(s => s.Age, config => config.MapFrom(u => u.Age));
        }
    }
}
