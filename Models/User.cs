using System.ComponentModel.DataAnnotations;

namespace StudentTranscriptPortal.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ? Role { get; set; }
    }
    
}
