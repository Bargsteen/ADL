using System.Collections.Generic;
using System.Linq;

namespace ADL.Models
{
    public class EFSchoolRepository : ISchoolRepository
    {
        private ApplicationDbContext context;

        public EFSchoolRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<School> Schools => context.Schools;

        public void SaveSchool(School school)
        {
            if(school.SchoolId == 0)
            {
                // New School
                context.Add(school);
            }
            else
            {
                // Update School
                School DbEntry = context.Schools.FirstOrDefault(s => s.SchoolId == school.SchoolId);
                if(DbEntry != null)
                {
                    DbEntry.SchoolName = school.SchoolName;
                    DbEntry.InstitutionNumber = school.InstitutionNumber;
                }
            }
            context.SaveChanges();
        }
        public School DeleteSchool(int schoolId)
        {
            School DbEntry = context.Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            if(DbEntry != null)
            {
                context.Remove(DbEntry);
                context.SaveChanges();
            }
            return DbEntry;
        }

        
    }
}
