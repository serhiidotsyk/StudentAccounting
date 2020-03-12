using BLL.Interfaces;
using BLL.Models.Course;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/course")]
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
        public IActionResult SubscribeToCourse(int userId, int courseId)
        {
            var course = _courseService.SubscribeToCourse(userId, courseId);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { message = "Couldnt subscribe to course" });
        }

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