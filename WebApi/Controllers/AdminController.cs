using BLL.Helpers.Pagination;
using BLL.Interfaces;
using BLL.Models.Auth;
using BLL.Models.StudentProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("getStudent")]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            var student = await _adminService.GetStudentAsync(studentId);
            //if (student != null)
            //{
                return Ok(student);
            //}
            //return BadRequest(new { title = "Couldnt find student" });
        }

        [HttpGet("getAllStudents")]
        public async Task<IActionResult> GetAllStudents([FromQuery] QueryStringParams queryStringParams)
        {
            var (student, count) = await _adminService.GetAllStudentsAsync(queryStringParams);
            if (student != null)
            {
                return Ok(new
                {
                    student,
                    count
                });
            }
            return BadRequest(new { title = "Couldnt find any student" });
        }

        [HttpPost("createStudent")]
        public async Task<IActionResult> CreateStudent(UserSignUpModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { title = "Model is not valid"});
            var student = await _adminService.CreateStudentAsync(userModel);
            if(student != null)
            {
                return Ok(student);
            }
            return BadRequest(new { title = "Couldnt create student" });
        }

        [HttpPut("updateStudent")]
        public async Task<IActionResult> UpdateStudent(UserModel studentModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { title = "Invalid model for update" });
            var student = await _adminService.UpdateStudentAsync(studentModel);
            if(student != null)
            {
                return Ok(student);
            }

            return BadRequest(new { title = "Couldnt update student" });
        }

        [HttpDelete("deleteStudent")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var student = await _adminService.DeleteAsync(studentId);
            if (student != null)
            {
                return Ok(student);
            }

            return BadRequest(new { title = "Couldnt delete student" });
        }

        [HttpDelete("deleteStudents")]
        public async Task<IActionResult> DeleteStudents([FromQuery] int[] studentIds)
        {
            var students = await _adminService.DeleteAsync(studentIds);
            if(students != null)
            {
                return Ok(students);
            }

            return BadRequest(new { title = "Couldnt delete students" });
        }

    }
}