using System.ComponentModel.DataAnnotations;

namespace StudentTranscriptPortal.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    
}
