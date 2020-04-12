using BLL.Interfaces;
using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { title = "Couldnt find course" });
        }

        [HttpGet("getCourses")]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            if (courses != null)
            {
                return Ok(courses);
            }

            return BadRequest(new { title = "Couldnt find course" });
        }

        [HttpGet("getAvailableCourses")]
        public async Task<IActionResult> GetAvailableCourses(int studentId)
        {
            var courses = await _courseService.GetAvailableCoursesAsync(studentId);
            if (courses != null)
            {
                return Ok(courses);
            }   

            return BadRequest(new { title = "Couldnt find course" });
        }

        [HttpGet("getCoursesByStudentId")]
        public async Task<IActionResult> GetCourseByStudentId(int studentId)
        {
            var courses = await _courseService.GetCoursesByStudentIdAsync(studentId);
            if(courses != null)
            {
                return Ok(courses);
            }

            return BadRequest(new { title = "Couldnt find course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("createCourse")]
        public async Task<IActionResult> CreateCourse(CourseModel courseModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var course = await _courseService.CreateCourseAsync(courseModel);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { title = "Couldnt create course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateCourse")]
        public async Task<IActionResult> UpdateCourse(CourseModel courseModel, int courseId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var course = await _courseService.UpdateCourseAsync(courseModel, courseId);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { title = "Couldnt update course" });
        }

        [HttpPut("subscribeToCourse")]
        public async Task<IActionResult> SubscribeToCourse(SubscribeToCourseModel subscribeToCourseModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var course = await _courseService.SubscribeToCourseAsync(subscribeToCourseModel);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { title = "Couldnt subscribe to course" });
        }

        [HttpPut("unSubscribeFromCourse")]
        public async Task<IActionResult> UnSubscribeFromCourse(UserCourseModel userCourseModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var course = await _courseService.UnSubscribeFromCourseAsync(userCourseModel);
            if(course != null)
            {
                return Ok(course);
            }
            return BadRequest(new { title = "Couldnt unsubscribe to course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteCourse")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _courseService.DeleteCourseAsync(id);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { title = "Couldnt delete course" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteCourses")]
        public async Task<IActionResult> DeleteCourses([FromQuery]int[] ids)
        {
            var course = await _courseService.DeleteCoursesAsync(ids);
            if (course != null)
            {
                return Ok(course);
            }

            return BadRequest(new { title = "Couldnt delete courses" });
        }

    }
}