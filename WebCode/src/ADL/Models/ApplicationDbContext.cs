using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ADL.Models.Answers;
using ADL.Models.Assignments;

namespace ADL.Models
{
    public class ApplicationDbContext : IdentityDbContext<Person>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public ApplicationDbContext() { }

        public virtual DbSet<AssignmentSet> AssignmentSets { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AnswerOption> AnswerOptions { get; set; }
        public virtual DbSet<AnswerBool> AnswerBools { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Class> Classes { get; set; }

    }
}
