using AutoMapper;
using BLL.Models.Auth;
using DAL.Entities;

namespace WebApi.MapperProfiler
{
    public class UserMapperProfile: Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserSignUpModel, User>();
            CreateMap<User, UserSignUpModel>();
        }
    }
}
