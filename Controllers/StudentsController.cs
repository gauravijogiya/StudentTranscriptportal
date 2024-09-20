using Microsoft.AspNetCore.Mvc;
using StudentTranscriptPortal.Data;
using StudentTranscriptPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentTranscriptPortal.Helpers;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentTranscriptPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        protected readonly StudentContext _context;

        
        public StudentsController(StudentContext context)
        {
            _context = context;
        }


        // GET: api/<StudentsController>
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Student>> GetStudentDetail(int id)
        {
            var studentDetail = await _context.Students.FindAsync(id);

            if (studentDetail == null)
            {
                return NotFound();
            }

            return studentDetail;
        }
        // POST: api/students
        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> PostStudent([FromForm] Student student,  IFormFile transcriptFile)
        // public async Task<IActionResult> PostStudent(Student student)
        {
            // Server-side validation for transcript
            if (transcriptFile == null || transcriptFile.Length == 0)
            {
                return BadRequest("Transcript is required.");
            }

            // Check if the transcript is a PDF
            //if (student.transcriptFile.ContentType != "application/pdf")
            //{
            //    return BadRequest("Only PDF files are allowed.");
            //}

            if (ModelState.IsValid)
            {
                Uploadhandler uploadhandler = new Uploadhandler(student.Name,transcriptFile);
                string fileName= uploadhandler.Upload(transcriptFile);
                //// Define the path to store the uploaded transcript
                //var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                //if (!Directory.Exists(uploadsPath))
                //{
                //    Directory.CreateDirectory(uploadsPath);
                //}

                //// Create a unique file name for the uploaded PDF
                //var fileName = $"{student.Name}_{Path.GetFileName(student.transcriptFile.FileName)}";
                //var filePath = Path.Combine(uploadsPath, fileName);

                //// Save the file
                //using (var fileStream = new FileStream(filePath, FileMode.Create))
                //{
                //    await student.transcriptFile.CopyToAsync(fileStream);
                //}

                // Store the file path in the student's data
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
               // var filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"), fileName);
                student.TranscriptPath = filePath;
                student.Name = "bb";

                // Save the student to the database
                _context.Students.Add(student);


                await _context.SaveChangesAsync();

                return Ok(new { student.Name, student.Email, student.TranscriptPath });


            }

            return BadRequest(ModelState);



        }
       
    }
}
