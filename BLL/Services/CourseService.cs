﻿using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using DAL;
using DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        private protected ApplicationDbContext _context;
        private protected IMapper _mapper;

        public CourseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public CourseModel SubscribeToCourse(int userId, int courseId)
        {
            var course = _context.Courses.Find(courseId);
            var user = _context.Users.Find(userId);
            if (course != null && user != null)
            {
                var userCourseModel = new UserCourseModel
                {
                    CourseId = course.Id,
                    UserId = user.Id
                };

                var userCourse = _mapper.Map<UserCourse>(userCourseModel);
                _context.UserCourses.Add(userCourse);

                user.StudyStart = course.StartDate;
                _context.Users.Update(user);
                _context.SaveChanges();

                return _mapper.Map<CourseModel>(course);
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

            if(courseList.Count > 1)
            {
                return courseList;
            }
            return null;
        }
    }
}