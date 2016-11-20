using Microsoft.EntityFrameworkCore;

namespace ADL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Location> Locations {get;set;}      
        public DbSet<Answer> Answers { get; set; }

    }
}
