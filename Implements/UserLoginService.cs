using Microsoft.EntityFrameworkCore;
using StudentTranscriptPortal.Models;
using StudentTranscriptPortal.Services;

namespace StudentTranscriptPortal.Implements
{
    
    public class UserLoginService:IUserLoginService
    {
        //InMemory USer
        public static List<User> Users = new List<User>
    {
        new User { UserId = "admin@dulux.com", Password = "admin123", Role = "Admin" },
        new User { UserId = "student@dulux.com", Password = "student123", Role = "Student" }
    };


        public async Task<User> GetUserLoginDetail(User user)
        {
            var userDetail =  Users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password && user.Role==u.Role);

            return userDetail;
        }
    }
}
