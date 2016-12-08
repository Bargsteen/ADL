using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ADL.Models.Answers;

namespace ADL.Models
{
    public class ApplicationDbContext : IdentityDbContext<Person>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<AssignmentSet> AssignmentSets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }

    }
}
