using Microsoft.EntityFrameworkCore;

namespace ADL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Assignment> Assigments { get; set; }
    }
}
