using AutoMapper;
using BLL.Models.Auth;
using DAL.Entities;

namespace WebApi.MapperProfiler
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserSignUpModel, User>();
            CreateMap<User, UserSignUpModel>();

            CreateMap<UserSocialLogin, User>()
                .ForMember(d => d.FirstName, s => s.MapFrom(um => FirstNameHelper(um.Name)))
                .ForMember(d => d.LastName, s => s.MapFrom(um => LastNameHelper(um.Name)));// { var subs = um.Name.Split(); return=subs[subs.Length-1] }));
            CreateMap<User, UserSocialLogin>();
        }

        public string FirstNameHelper(string name)
        {
            var result = name.Split(' ');
            return result[0];
        }
        public string LastNameHelper(string name)
        {
            var result = name.Split(' ');
            return result[result.Length - 1];
        }
    }
}
