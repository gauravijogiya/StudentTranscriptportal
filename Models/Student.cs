using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentTranscriptPortal.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string ContactNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string TranscriptYear { get; set; }  // This could be file upload logic in a more advanced app
        [Column(TypeName = "nvarchar(150)")]
        
        public string ? TranscriptPath { get; set; }
        [NotMapped]
        public IFormFile Transcript{ get; set; }
    }
}
