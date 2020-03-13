using AutoMapper;
using BLL.Interfaces;
using BLL.Models.StudentProfile;
using DAL;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class AdminService : IAdminService
    {
        private protected ApplicationDbContext _context;
        private protected IMapper _mapper;
        public AdminService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StudentInfoModel GetStudent(int studentId)
        {
            var student = _context.Users.Find(studentId);
            if (student != null)
            {
                return _mapper.Map<StudentInfoModel>(student);
            }

            return null;
        }

        public ICollection<StudentInfoModel> GetAllStudents()
        {
            var students = _context.Users.ToList();
            if (students != null)
            {
                return _mapper.Map<ICollection<StudentInfoModel>>(students);
            }

            return null;
        }

        public StudentModel UpdateStudent(StudentModel studentModel, int studentId)
        {
            var student = _context.Users.Find(studentId);
            if (student != null)
            {
                student = _mapper.Map<User>(studentModel);
                _context.Users.Update(student);
                _context.SaveChanges();

                return studentModel;
            }

            return null;
        }

        public StudentModel Delete(int studentId)
        {
            var studentToDelete = _context.Users.Find(studentId);
            if (studentToDelete != null)
            {
                _context.Users.Remove(studentToDelete);
                _context.SaveChanges();
                return _mapper.Map<StudentModel>(studentToDelete);
            }

            return null;
        }

        public ICollection<StudentModel> Delete(int[] studentIds)
        {
            List<StudentModel> studentList = new List<StudentModel>();
            foreach (var id in studentIds)
            {
                var studentToDelete = _context.Users.Find(id);
                if (studentToDelete != null)
                {
                    _context.Users.Remove(studentToDelete);
                    _context.SaveChanges();

                    var studentModel = _mapper.Map<StudentModel>(studentToDelete);

                    studentList.Add(studentModel);
                }
            }

            if (studentList.Count > 1)
            {
                return studentList;
            }

            return null;
        }
    }
}
