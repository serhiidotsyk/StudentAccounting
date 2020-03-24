using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using BLL.Services.Extensions;
using DAL;
using DAL.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public CourseModel GetCourse(int id)
        {
            var course = _context.Courses.Find(id);
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
        public ICollection<CourseModel> GetCoursesByStudentId(int studentId)
        {
            var courses = _context.UserCourses.Include(c => c.Course)
                                                .Where(u => u.UserId == studentId)
                                                  .Select(x => x.Course);
            if (courses != null)
            {
                return _mapper.Map<ICollection<CourseModel>>(courses);
            }

            return null;
        }

        /// <summary>
        /// gets all courses
        /// </summary>
        /// <returns>
        /// returns a collection of all courses
        /// </returns>
        public ICollection<CourseModel> GetAllCourses()
        {
            var courses = _context.Courses.ToList();
            if (courses != null)
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
        public CourseModel CreateCourse(CourseModel createCourseModel)
        {
            var course = _mapper.Map<Course>(createCourseModel);
            _context.Courses.Add(course);
            _context.SaveChanges();

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
        public CourseModel UpdateCourse(CourseModel courseModel, int courseId)
        {
            var course = _context.Courses.Find(courseId);
            if (course != null)
            {
                course = _mapper.Map<Course>(courseModel);
                _context.Courses.Update(course);
                _context.SaveChanges();

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
        public CourseModel SubscribeToCourse(SubscribeToCourseModel subscribeToCourseModel)
        {
            var course = _context.Courses.Find(subscribeToCourseModel.CourseId);
            var user = _context.Users.Find(subscribeToCourseModel.UserId);

            if (course != null && user != null)
            {
                var userCourseModel = new UserCourseModel
                {
                    CourseId = course.Id,
                    UserId = user.Id
                };

                var userCourse = _mapper.Map<UserCourse>(userCourseModel);
                _context.UserCourses.Add(userCourse);

                course.StartDate = subscribeToCourseModel.EnrollmentDate;
                course.EndDate = course.StartDate.Value.AddDays((double)course.DurationDays);
                _context.Courses.Update(course);

                user.StudyStart = course.StartDate;
                _context.Users.Update(user);

                _context.SaveChanges();

                SendScheduledEmailBackground(course.StartDate.Value, ONE_DAY_BEFORE_COURSE_START);
                SendScheduledEmailBackground(course.StartDate.Value, ONE_WEEK_BEFORE_COURSE_START);
                SendScheduledEmailBackground(course.StartDate.Value, ONE_MONTH_BEFORE_COURSE_START);

                return _mapper.Map<CourseModel>(course);
            }

            return null;
        }

        private void SendScheduledEmailBackground(DateTime startDate, int delayInDays)
        {
            var timeToSendEmail = startDate.AddDays(delayInDays * -1);
            if (delayInDays == ONE_DAY_BEFORE_COURSE_START)
            {
                timeToSendEmail = new DateTime(timeToSendEmail.Year, timeToSendEmail.Month, timeToSendEmail.Day, HOUR_TO_SEND_EMAIL, 0, 0);
            }

            DateTimeOffset offsetTime = new DateTimeOffset(timeToSendEmail);
            _backgroundJob.Schedule<IMailService>(mailService => mailService.SendScheduledEmail($"test scheduled in {delayInDays} day(-s)"), offsetTime);
        }


        /// <summary>
        /// deletes a course
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// returns a deleted course
        /// </returns>
        public CourseModel DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
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
        public ICollection<CourseModel> DeleteCourses(int[] ids)
        {
            List<CourseModel> courseList = new List<CourseModel>();
            foreach (var id in ids)
            {
                var course = _context.Courses.Find(id);
                if (course != null)
                {
                    _context.Courses.Remove(course);
                    _context.SaveChanges();

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
