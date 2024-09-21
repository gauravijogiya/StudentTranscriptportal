using StudentTranscriptPortal.Models;
using StudentTranscriptPortal.Helpers;
namespace StudentTranscriptPortal.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentDetail(int id);
        Task<APIResponse> PostStudent(Student data);
    }
}
