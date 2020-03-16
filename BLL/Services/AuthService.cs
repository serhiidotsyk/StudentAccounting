using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using DAL;
using DAL.Entities;
using DAL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AuthService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public (bool, UserModel) SignIn(UserSignInModel userSignInModel)
        {
            var user = _context.Users.Where(u => u.Email == userSignInModel.Email).SingleOrDefault();
            if (user == null)
            {
                return (false, null);
            }
            if (user.Password.Equals(userSignInModel.Password))
            {
                return (true, _mapper.Map<UserModel>(user));
            }
            else
            {
                return (false, null);
            }

        }

        public UserModel SignUp(UserSignUpModel userSignUpModel)
        {
            var user = _mapper.Map<UserSignUpModel, User>(userSignUpModel, cfg =>
                cfg.AfterMap((src, dest) =>
                {
                    dest.RegisteredDate = DateTime.Now;
                    dest.RoleId = (int)RoleType.Student;
                }));

            if (user != null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return _mapper.Map<UserModel>(user);
            }

            return null;
        }
    }
}
