using AutoMapper;
using BLL.Interfaces;
using BLL.Models.StudentProfile;
using DAL;
using System;

namespace BLL.Services
{
    public class StudentProfileService : IStudentProfileService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public StudentProfileService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public StudentEditProfileModel EditStudentProfile(int id)
        {
            throw new NotImplementedException();
        }

        public StudentProfileModel GetStudentProfile(int id)
        {
            var student = _dbContext.Users.Find(id);
            if (student != null)
            {
                return _mapper.Map<StudentProfileModel>(student);
            }                    
            return null;           
        }
    }
}
