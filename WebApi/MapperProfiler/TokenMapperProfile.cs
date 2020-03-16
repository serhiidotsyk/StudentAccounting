using AutoMapper;
using BLL.Models.Token;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.MapperProfiler
{
    public class TokenMapperProfile: Profile
    {
        public TokenMapperProfile()
        {
            CreateMap<RefreshToken, RefreshTokenModel>();
            CreateMap<RefreshTokenModel, RefreshToken>();
        }
    }
}
