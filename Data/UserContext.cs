using Microsoft.EntityFrameworkCore;
using StudentTranscriptPortal.Models;
namespace StudentTranscriptPortal.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public virtual DbSet<User>? Users { get; set; }
    }
    

   
}
