using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentProfileService _studentService;

        public StudentController(IStudentProfileService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("getProfile")]
        public IActionResult GetStudentProfile(int id)
        {
            var student = _studentService.GetStudentProfile(id);
            
            if (student != null)
                return Ok(student);

            return BadRequest(new { title = "Couldnt find student" });
        }
    }
}