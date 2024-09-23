using StudentTranscriptPortal.Models;

namespace StudentTranscriptPortal.Services
{
    public interface IUserLoginService
    {
        Task<User> GetUserLoginDetail(User user);
    }
}
