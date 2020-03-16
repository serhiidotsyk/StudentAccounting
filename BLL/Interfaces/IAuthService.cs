using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public (bool, UserModel) SignIn(UserSignInModel userSignInModel);
        public UserModel SignUp(UserSignUpModel userSignUpModel);
    }
}
