using System.Collections.Generic;
using System.Linq;

namespace ADL.Models.Repositories
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
                context.Schools.Add(school);
            }
            else
            {
                // Update School
                School DbEntry = Schools.FirstOrDefault(s => s.SchoolId == school.SchoolId);
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
            School DbEntry = Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            if(DbEntry != null)
            {
                context.Schools.Remove(DbEntry);
                context.SaveChanges();
            }
            return DbEntry;
        }

        
    }
}
