using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using DAL;
using DAL.Entities;
using DAL.Shared;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class AdminService : IAdminService
    {
        private protected ApplicationDbContext _context;
        private protected IMapper _mapper;
        private readonly BackgroundJobClient _backgroundJob;
        public AdminService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _backgroundJob = new BackgroundJobClient();
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

        public UserModel CreateStudent(UserSignUpModel studentModel)
        {
            var isUserExists = _context.Users.Where(u => u.Email == studentModel.Email).SingleOrDefault();
            if (isUserExists != null)
                return null;

            var user = _mapper.Map<UserSignUpModel, User>(studentModel, cfg =>
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

                //string callBackUrl = _mailService.GenerateConfirmationLink(userValidation);

                //_mailService.SendConfirmationLink(userValidation.Email,
                //    $"Confirm registration following the link: <a href='{callBackUrl}'>Confirm email NOW</a>");

                return userValidation;
            }

            return null;
        }

        public UserModel UpdateStudent(UserModel studentModel)
        {
            var student = _context.Users.Find(studentModel.Id);
            if (student != null)
            {
                student = _mapper.Map<UserModel, User>(studentModel, student);
                _context.Users.Update(student);
                _context.SaveChanges();

                return studentModel;
            }

            return null;
        }

        public UserModel Delete(int studentId)
        {
            var studentToDelete = _context.Users.Include(u => u.ScheduledJobs).Where(u=>u.Id == studentId).SingleOrDefault();
            if (studentToDelete != null)
            {
                foreach(var item in studentToDelete.ScheduledJobs)
                {
                    _backgroundJob.Delete(item.Id);
                    _context.ScheduledJobs.Remove(item);
                }
                _context.Users.Remove(studentToDelete);
                _context.SaveChanges();
                return _mapper.Map<UserModel>(studentToDelete);
            }

            return null;
        }

        public ICollection<UserModel> Delete(int[] studentIds)
        {
            List<UserModel> studentList = new List<UserModel>();
            foreach (var id in studentIds)
            {
                var studentToDelete = _context.Users.Find(id);
                if (studentToDelete != null)
                {
                    foreach (var item in studentToDelete.ScheduledJobs)
                    {
                        _backgroundJob.Delete(item.Id);
                        _context.ScheduledJobs.Remove(item);
                    }

                    _context.Users.Remove(studentToDelete);
                    _context.SaveChanges();

                    var studentModel = _mapper.Map<UserModel>(studentToDelete);

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
