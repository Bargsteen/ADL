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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TextAnswer>();
            builder.Entity<ExclusiveChoiceAnswer>();
            builder.Entity<MultipleChoiceAnswer>();

            builder.Entity<TextAssignment>();
            builder.Entity<MultipleChoiceAssignment>();
            builder.Entity<ExclusiveChoiceAssignment>();

            base.OnModelCreating(builder);
        }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentSet> AssignmentSets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<AnswerBool> AnswerBools { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }

    }
}
