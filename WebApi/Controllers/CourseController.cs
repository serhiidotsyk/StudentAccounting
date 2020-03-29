using BLL.Interfaces;
using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/course")]
    [Authorize]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private protected ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("getCourse")]
        public IActionResult GetCourse(int id)
        {
            var course = _courseService.GetCourse(id);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { message = "Couldnt find course" });
        }

        [HttpGet("getCourses")]
        public IActionResult GetCourses()
        {
            var courses = _courseService.GetAllCourses();
            if (courses != null)
            {
                return Ok(courses);
            }

            return BadRequest(new { message = "Couldnt find course" });
        }

        [HttpGet("getAvailableCourses")]
        public IActionResult GetAvailableCourses(int studentId)
        {
            var courses = _courseService.GetAvailableCourses(studentId);
            if (courses != null)
            {
                return Ok(courses);
            }   

            return BadRequest(new { message = "Couldnt find course" });
        }

        [HttpGet("getCoursesByStudentId")]
        public IActionResult GetCourseByStudentId(int studentId)
        {
            var courses = _courseService.GetCoursesByStudentId(studentId);
            if(courses != null)
            {
                return Ok(courses);
            }

            return BadRequest(new { message = "Couldnt find course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createCourse")]
        public IActionResult CreateCourse(CourseModel courseModel)
        {
            var course = _courseService.CreateCourse(courseModel);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { message = "Couldnt create course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateCourse")]
        public IActionResult UpdateCourse(CourseModel courseModel, int courseId)
        {
            var course = _courseService.UpdateCourse(courseModel, courseId);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { message = "Couldnt update course" });
        }

        [HttpPut("subscribeToCourse")]
        public IActionResult SubscribeToCourse(SubscribeToCourseModel subscribeToCourseModel)
        {
            var course = _courseService.SubscribeToCourse(subscribeToCourseModel);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { message = "Couldnt subscribe to course" });
        }

        [HttpPut("unSubscribeFromCourse")]
        public IActionResult UnSubscribeFromCourse(UserCourseModel userCourseModel)
        {
            var course = _courseService.UnSubscribeFromCourse(userCourseModel);
            if(course != null)
            {
                return Ok(course);
            }
            return BadRequest(new { message = "Couldnt unsubscribe to course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteCourse")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _courseService.DeleteCourse(id);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { message = "Couldnt delete course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteCourses")]
        public IActionResult DeleteCourses([FromQuery]int[] ids)
        {
            var course = _courseService.DeleteCourses(ids);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { message = "Couldnt delete courses" });
        }

    }
}