using BLL.Interfaces;
using BLL.Models.StudentProfile;
using DAL;
using System;

namespace BLL.Services
{
    class UserProfileService : IUserProfile
    {
        private readonly ApplicationDbContext _dbContext;
        public UserProfileService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public StudentEditProfileModel EditStudentProfile(int id)
        {
            throw new NotImplementedException();
        }

        public StudentProfileModel GetStudentProfile(int id)
        {
            throw new NotImplementedException();
        }
    }
}
