 using StudentTranscriptPortal.Data;
using StudentTranscriptPortal.Models;
using Microsoft.AspNetCore.Mvc;
using StudentTranscriptPortal.Services;
using StudentTranscriptPortal.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace StudentTranscriptPortal.Implements
{
    public class StudentService : IStudentService
    {
        protected readonly StudentContext _context;
        public StudentService(StudentContext context) { _context = context; }

        /*it gets student by Id :Created to check if data inserted succesfully or not*/
        public async Task<Student> GetStudentDetail(int id)
        {
           var studentDetail = await _context.Students.FindAsync(id);

            return studentDetail;
        }

        /*Inserts students data into database and uploads Transcript*/
        public async Task<APIResponse> PostStudent(Student student)
        {
            APIResponse response = new APIResponse();
            try
            {                
                Uploadhandler uploadhandler = new Uploadhandler(student.Name, student.Transcript);
                if (uploadhandler.checkExtension(student.Transcript.FileName))
                {
                    string fileName = uploadhandler.Upload(student.Transcript);
                    // Store the file path in the student's data
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
                    student.TranscriptPath = filePath;
                    // Save the student to the database
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();

                    response.ResponseCode = 201;
                    response.Message = "Thank you " + student.Name + " for submitting your Transcript .";
                }
                else {
                    response.ResponseCode = 422;
                    response.Message = "Unsupported file format";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.Message = ex.Message;                
            }
             return response;

        }
    }
}
