using AutoMapper;
using BLL.Interfaces;
using BLL.Models.StudentProfile;
using DAL;
using System;

namespace BLL.Services
{
    public class StudentProfileService : IStudentProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentProfileService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StudentModel EditStudentProfile(int id)
        {
            throw new NotImplementedException();
        }

        public StudentModel GetStudentProfile(int id)
        {
            var student = _context.Users.Find(id);
            if (student != null)
            {
                return _mapper.Map<StudentModel>(student);
            }                    

            return null;           
        }
    }
}
