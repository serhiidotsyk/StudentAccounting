using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using DAL;
using DAL.Entities;
using DAL.Shared;
using System;
using System.Linq;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        public AuthService(ApplicationDbContext context, IMapper mapper, IMailService mailService)
        {
            _context = context;
            _mapper = mapper;
            _mailService = mailService;
        }
        public (bool, UserModel) SignIn(UserSignInModel userSignInModel)
        {
            var user = _context.Users.Where(u => u.Email == userSignInModel.Email).SingleOrDefault();
            if (user == null)
            {
                return (false, null);
            }
            if (user.Password.Equals(userSignInModel.Password) && user.IsEmailComfirmed == true)
            {
                return (true, _mapper.Map<UserModel>(user));
            }
            else
            {
                return (false, null);
            }

        }

        public (bool, UserModel) SocialLogin(UserSocialLogin userSocialLogin)
        {
            var user = _context.Users.Where(u => u.Email == userSocialLogin.Email).SingleOrDefault();

            if (user == null)
            {
                return (true, SocialSignUp(userSocialLogin));
            }
            if (user.Email == userSocialLogin.Email)
            {
                return (true, _mapper.Map<UserModel>(user));
            }
            else
            {
                return (false, null);
            }
        }

        private UserModel SocialSignUp(UserSocialLogin userSocialLogin)
        {
            var user = _mapper.Map<UserSocialLogin, User>(userSocialLogin, cfg =>
                cfg.AfterMap((src, dest) =>
                {
                    dest.RegisteredDate = DateTime.Now;
                    dest.Age = new DateTime((DateTime.UtcNow - Convert.ToDateTime(src.BirthDate)).Ticks).Year;
                    dest.RoleId = (int)RoleType.Student;
                }));

            if(user != null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return _mapper.Map<UserModel>(user);
            }
            return null;
        }

        public UserModel SignUp(UserSignUpModel userSignUpModel)
        {
            var isUserExists = _context.Users.Where(u => u.Email == userSignUpModel.Email).SingleOrDefault();
            if (isUserExists != null)
                return null;

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

                var userValidation = _mapper.Map<UserModel>(user);

                string callBackUrl = _mailService.GenerateConfirmationLink(userValidation);

                _mailService.SendConfirmationLink(userValidation.Email,
                    $"Confirm registration following the link: <a href='{callBackUrl}'>Confirm email NOW</a>");

                return userValidation;
            }

            return null;
        }

        public UserModel ConfirmEmail(int userId, string token)
        {
            var user = _context.Users.Where(u => u.Id == userId).SingleOrDefault();
            if(user != null)
            {
                if (user.EmailConfirmationToken.Equals(token))
                {
                    user.IsEmailComfirmed = true;

                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
                return _mapper.Map<UserModel>(user);
            }
            return null;
            
        }
    }
}
