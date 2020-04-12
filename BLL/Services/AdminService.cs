using AutoMapper;
using BLL.Exceptions;
using BLL.Helpers.Pagination;
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
using System.Threading.Tasks;

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

        public async Task<StudentInfoModel> GetStudentAsync(int studentId)
        {
            var student = await _context.Users.FindAsync(studentId);
            if (student != null)
            {
                return _mapper.Map<StudentInfoModel>(student);
            }

            throw new NotFoundException("User was not found");
        }

        public async Task<(ICollection<StudentInfoModel>, int)> GetAllStudentsAsync(QueryStringParams queryStringParams)
        {
            IEnumerable<User> students = await _context.Users.Include(u => u.UserCourses)
                                                              .ThenInclude(uc => uc.Course)
                                                               .Where(u => u.RoleId == (int)RoleType.Student)
                                                                .ToListAsync();

            if (!string.IsNullOrEmpty(queryStringParams.SearchString))
            {
                var searchQuery = queryStringParams.SearchString.Split(' ');
                int age = 0, count = 0;
                for (int i = 0; i < searchQuery.Length; i++)
                {
                    if (int.TryParse(searchQuery[i], out age))
                    {
                        count++;
                    }
                }
                if (count > 1)
                    return (null, 0);

                if (count == 1 && age > 0)
                {
                    students = students.Where(s => s.FirstName.Contains(queryStringParams.SearchString)
                                                  || s.LastName.Contains(queryStringParams.SearchString)
                                                   || s.Email.Contains(queryStringParams.SearchString)
                                                    || s.Age == age);
                }
                else
                {
                    students = students.Where(s => s.FirstName.Contains(queryStringParams.SearchString)
                                                  || s.LastName.Contains(queryStringParams.SearchString)
                                                   || s.Email.Contains(queryStringParams.SearchString));
                }
            }

            if (!string.IsNullOrEmpty(queryStringParams.SortOrder) && !string.IsNullOrEmpty(queryStringParams.SortField))
            {
                queryStringParams.SortField = char.ToUpper(queryStringParams.SortField[0])
                                                    + queryStringParams.SortField.Substring(1);

                var field = typeof(User).GetProperty(queryStringParams.SortField);

                switch (queryStringParams.SortOrder)
                {
                    case "ascend":
                        {
                            students = students.OrderBy(x => field.GetValue(x, null));
                        }
                        break;
                    case "descend":
                        {
                            students = students.OrderByDescending(x => field.GetValue(x, null));
                        }
                        break;
                    default:
                        break;
                }
            }
            var studentsResult = PaginationHelper<User>.GetPageValues(students, queryStringParams.PageSize, queryStringParams.PageNumber);

            if (students != null && studentsResult != null)
            {
                return (_mapper.Map<ICollection<StudentInfoModel>>(studentsResult), students.Count());
            }

            return (null, 0);
        }

        public async Task<UserModel> CreateStudentAsync(UserSignUpModel studentModel)
        {
            var isUserExists = await _context.Users.Where(u => u.Email == studentModel.Email)
                                                    .SingleOrDefaultAsync();
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
                await _context.SaveChangesAsync();

                return _mapper.Map<UserModel>(user);
            }

            return null;
        }

        public async Task<UserModel> UpdateStudentAsync(UserModel studentModel)
        {
            var student = await _context.Users.FindAsync(studentModel.Id);
            if (student != null)
            {
                student = _mapper.Map<UserModel, User>(studentModel, student);

                _context.Users.Update(student);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserModel>(student);
            }

            return null;
        }

        public async Task<UserModel> DeleteAsync(int studentId)
        {
            var studentToDelete = await _context.Users.Include(u => u.ScheduledJobs)
                                                       .Where(u => u.Id == studentId)
                                                        .SingleOrDefaultAsync();
            if (studentToDelete != null)
            {
                foreach (var item in studentToDelete.ScheduledJobs)
                {
                    _backgroundJob.Delete(item.Id);
                    _context.ScheduledJobs.Remove(item);
                }
                _context.Users.Remove(studentToDelete);
                await _context.SaveChangesAsync();

                return _mapper.Map<UserModel>(studentToDelete);
            }

            return null;
        }

        public async Task<ICollection<UserModel>> DeleteAsync(int[] studentIds)
        {
            List<UserModel> studentList = new List<UserModel>();
            foreach (var id in studentIds)
            {
                var studentToDelete = await _context.Users.FindAsync(id);
                if (studentToDelete != null)
                {
                    foreach (var item in studentToDelete.ScheduledJobs)
                    {
                        _backgroundJob.Delete(item.Id);
                        _context.ScheduledJobs.Remove(item);
                    }

                    _context.Users.Remove(studentToDelete);
                    await _context.SaveChangesAsync();

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
