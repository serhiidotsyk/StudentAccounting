using BLL.Interfaces;
using BLL.Models.StudentProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{    
    [Route("api/admin")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("getStudent")]
        public IActionResult GetStudent(int studentId)
        {
            var student = _adminService.GetStudent(studentId);
            if (student != null)
            {
                return Ok(student);
            }
            return BadRequest(new { message = "Couldnt find student" });
        }

        [HttpGet("getAllStudents")]
        public IActionResult GetAllStudents()
        {
            var student = _adminService.GetAllStudents();
            if (student != null)
            {
                return Ok(student);
            }
            return BadRequest(new { message = "Couldnt find any student" });
        }

        [HttpPut("updateStudent")]
        public IActionResult UpdateStudent(UserModel studentModel, int studentId)
        {
            var student = _adminService.UpdateStudent(studentModel, studentId);
            if(student != null)
            {
                return Ok(student);
            }

            return BadRequest(new { message = "Couldnt update student" });
        }

        [HttpDelete("deleteStudent")]
        public IActionResult DeleteStudent(int studentId)
        {
            var student = _adminService.Delete(studentId);
            if (student != null)
            {
                return Ok(student);
            }

            return BadRequest(new { message = "Couldnt delete student" });
        }

        [HttpDelete("deleteStudents")]
        public IActionResult DeleteStudents([FromQuery] int[] studentIds)
        {
            var students = _adminService.Delete(studentIds);
            if(students != null)
            {
                return Ok(students);
            }

            return BadRequest(new { message = "Couldnt delete students" });
        }

    }
}