using Microsoft.AspNetCore.Mvc;
using StudentTranscriptPortal.Data;
using StudentTranscriptPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentTranscriptPortal.Helpers;
using Microsoft.AspNetCore.Authorization;
using StudentTranscriptPortal.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentTranscriptPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
       // protected readonly StudentContext _context;
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<Student>> GetStudentDetail(int id)
        {
            var studentDetail = await _service.GetStudentDetail(id);

            if (studentDetail == null)
            {
                return NotFound();
            }

            return studentDetail;
        }
        // POST: api/students
        [HttpPost]
       // [Authorize]
       // public async Task<IActionResult> PostStudent([FromForm] Student student, [FromForm] IFormFile transcriptFile)
        public async Task<IActionResult> PostStudent(Student student)
        {
            var transcriptFile = student.Transcript; ;
            // Server-side validation for transcript
            if (transcriptFile == null || transcriptFile.Length == 0)
            {
                return BadRequest("Transcript is required.");
            }

            if (ModelState.IsValid)
            {
                var data = await _service.PostStudent(student);

                if (data.ResponseCode == 422) {
                    return BadRequest(data.Message);
                }
                return Ok(data.Message);
            }
            return BadRequest(ModelState);



        }
       
    }
}
