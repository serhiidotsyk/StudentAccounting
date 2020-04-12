using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Course;
using BLL.Models.ScheduledJob;
using BLL.Models.UserCourseModel;
using DAL;
using DAL.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly BackgroundJobClient _backgroundJob;


        private const int ONE_DAY_BEFORE_COURSE_START = 1;
        private const int ONE_WEEK_BEFORE_COURSE_START = 7;
        private const int ONE_MONTH_BEFORE_COURSE_START = 30;
        private const int HOUR_TO_SEND_EMAIL = 8;



        public CourseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _backgroundJob = new BackgroundJobClient();
        }

        /// <summary>
        /// Gets course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// returns course model
        /// </returns>
        public async Task<CourseModel> GetCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                return _mapper.Map<CourseModel>(course);
            }

            return null;
        }

        /// <summary>
        /// gets courses by user id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        /// return collection of courses by student
        /// </returns>
        public async Task<ICollection<CourseInfoModel>> GetCoursesByStudentIdAsync(int studentId)
        {
            ICollection<UserCourse> courses = await _context.UserCourses
                                                                        .Include(c => c.Course)
                                                                         .Where(u => u.StudentId == studentId)
                                                                          .ToListAsync();

            if (courses?.Count() > 0)
            {
                return _mapper.Map<ICollection<CourseInfoModel>>(courses);
            }

            return null;
        }

        /// <summary>
        /// gets all courses
        /// </summary>
        /// <returns>
        /// returns a collection of all courses
        /// </returns>
        public async Task<ICollection<CourseModel>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            if (courses != null)
            {
                return _mapper.Map<ICollection<CourseModel>>(courses);
            }

            return null;
        }

        /// <summary>
        /// get avalilable courses for specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// returns a collection of available courses
        /// </returns>
        public async Task<ICollection<CourseModel>> GetAvailableCoursesAsync(int userId)
        {
            var courses = await _context.Courses.Include(c => c.UserCourses)
                                            .Where(uc => uc.UserCourses
                                             .All(u => u.StudentId != userId) || uc.UserCourses.Count == 0)
                                              .ToListAsync();

            if (courses != null && courses.Count() > 0)
            {
                return _mapper.Map<ICollection<CourseModel>>(courses);
            }

            return null;
        }

        /// <summary>
        /// creates a course
        /// </summary>
        /// <param name="createCourseModel"></param>
        /// <returns>
        /// returns a created course model
        /// </returns>
        public async Task<CourseModel> CreateCourseAsync(CourseModel createCourseModel)
        {
            var course = _mapper.Map<Course>(createCourseModel);
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseModel>(course);
        }

        /// <summary>
        /// updates course
        /// </summary>
        /// <param name="courseModel"></param>
        /// <param name="courseId"></param>
        /// <returns>
        /// returns an updated course model
        /// </returns>
        public async Task<CourseModel> UpdateCourseAsync(CourseModel courseModel, int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                course = _mapper.Map<Course>(courseModel);
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();

                return courseModel;
            }

            return null;
        }

        /// <summary>
        /// subscribes a user to a course
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns>
        /// returns a course to which user was subscribed
        /// </returns>
        public async Task<CourseModel> SubscribeToCourseAsync(SubscribeToCourseModel subscribeToCourseModel)
        {
            var course = await _context.Courses.FindAsync(subscribeToCourseModel.CourseId);
            var user = await _context.Users.FindAsync(subscribeToCourseModel.StudentId);

            if (course != null && user != null)
            {
                var startingDate = DateTimeOffset.FromUnixTimeMilliseconds(subscribeToCourseModel.EnrollmentDate).UtcDateTime;
                var userCourseModel = new UserCourseModel
                {
                    CourseId = course.Id,
                    StudentId = user.Id,
                    StartDate = startingDate,
                    EndDate = startingDate.AddDays((double)course.DurationDays)
                };

                var userCourse = _mapper.Map<UserCourse>(userCourseModel);
                _context.UserCourses.Add(userCourse);
                await _context.SaveChangesAsync();

                await SendScheduledEmailBackgroundAsync(userCourseModel.StartDate, user.Email, user.Id);

                return _mapper.Map<CourseModel>(course);
            }

            return null;
        }

        private async Task SendScheduledEmailBackgroundAsync(DateTime startDate, string email, int userId)
        {
            var oneDayBeforeEmail = startDate.AddDays(ONE_DAY_BEFORE_COURSE_START * -1);
            var oneWeekBeforeEmail = startDate.AddDays(ONE_WEEK_BEFORE_COURSE_START * -1);
            var oneMonthBeforeEmail = startDate.AddDays(ONE_MONTH_BEFORE_COURSE_START * -1);

            var days = startDate - DateTime.UtcNow;

            List<ScheduledJobModel> jobModelList = new List<ScheduledJobModel>();

            if (days.Days >= 30)
            {
                var key = _backgroundJob.Schedule<IMailService>(mailService => mailService
                                     .SendScheduledEmail(email, $"test scheduled in {ONE_MONTH_BEFORE_COURSE_START} day(-s)"),
                                     new DateTimeOffset(oneMonthBeforeEmail));

                jobModelList.Add(new ScheduledJobModel { Key = key });
            }

            if (days.Days >= 14)
            {
                var key = _backgroundJob.Schedule<IMailService>(mailService => mailService
                                     .SendScheduledEmail(email, $"test scheduled in {ONE_WEEK_BEFORE_COURSE_START} day(-s)"),
                                     new DateTimeOffset(oneWeekBeforeEmail));

                jobModelList.Add(new ScheduledJobModel { Key = key });
            }

            if (days.Days >= 1)
            {
                oneDayBeforeEmail = new DateTime(oneDayBeforeEmail.Year, oneDayBeforeEmail.Month, oneDayBeforeEmail.Day, HOUR_TO_SEND_EMAIL, 0, 0);
                var key = _backgroundJob.Schedule<IMailService>(mailService => mailService
                                     .SendScheduledEmail(email, $"test scheduled in {ONE_DAY_BEFORE_COURSE_START} day(-s)"),
                                     new DateTimeOffset(oneDayBeforeEmail));

                _backgroundJob.Enqueue<IMailService>(mailService => mailService
                                     .SendScheduledEmail(email, $"test scheduled in {days.Days} day(-s)"));
                jobModelList.Add(new ScheduledJobModel { Key = key });
            }

            if (jobModelList.Count > 0)
            {
                for (int i = 0; i < jobModelList.Count; i++)
                {
                    _context.ScheduledJobs.Add(new ScheduledJob { Id = jobModelList[i].Key, UserId = userId });
                }
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// unsubscribes user from course
        /// </summary>
        /// <param name="userCourseModel"></param>
        /// <returns>
        /// unsubscribed course
        /// </returns>
        public async Task<UserCourseModel> UnSubscribeFromCourseAsync(UserCourseModel userCourseModel)
        {
            var userCourse = await _context.UserCourses.Where(uc => uc.StudentId == userCourseModel.StudentId
                                                           && uc.CourseId == userCourseModel.CourseId)
                                                            .SingleOrDefaultAsync();
            if (userCourse != null)
            {
                var user = await _context.Users.Include(u => u.ScheduledJobs)
                                          .Where(u => u.Id == userCourseModel.StudentId)
                                           .SingleOrDefaultAsync();
                if (user != null)
                {
                    foreach (var item in user.ScheduledJobs)
                    {
                        _backgroundJob.Delete(item.Id);
                        _context.ScheduledJobs.Remove(item);
                    }
                }

                _context.UserCourses.Remove(userCourse);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserCourseModel>(userCourse);
            }
            return null;
        }

        /// <summary>
        /// deletes a course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// returns a deleted course
        /// </returns>
        public async Task<CourseModel> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return _mapper.Map<CourseModel>(course);
            }

            return null;
        }

        /// <summary>
        /// deletes a collection of courses
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>
        /// returns a collection of deleted courses
        /// </returns>
        public async Task<ICollection<CourseModel>> DeleteCoursesAsync(int[] ids)
        {
            List<CourseModel> courseList = new List<CourseModel>();
            foreach (var id in ids)
            {
                var course = await _context.Courses.FindAsync(id);
                if (course != null)
                {
                    _context.Courses.Remove(course);
                    await _context.SaveChangesAsync();

                    var courseModel = _mapper.Map<CourseModel>(course);

                    courseList.Add(courseModel);
                }
            }

            if (courseList.Count > 1)
            {
                return courseList;
            }
            return null;
        }
    }
}
