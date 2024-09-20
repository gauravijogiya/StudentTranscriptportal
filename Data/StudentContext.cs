using Microsoft.EntityFrameworkCore;
using StudentTranscriptPortal.Models;

namespace StudentTranscriptPortal.Data
{
    public class StudentContext:DbContext
    {
        public StudentContext(DbContextOptions<StudentContext>option):base(option) { }
       public DbSet<Student> Students { get; set; }
    }
}
