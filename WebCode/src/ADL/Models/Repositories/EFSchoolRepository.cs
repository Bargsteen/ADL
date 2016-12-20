using System.Collections.Generic;
using System.Linq;

namespace ADL.Models.Repositories
{
    public class EfSchoolRepository : ISchoolRepository
    {
        private readonly ApplicationDbContext _context;

        public EfSchoolRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<School> Schools => _context.Schools;

        public void SaveSchool(School school)
        {
            if(school.SchoolId == 0)
            {
                // New School
                _context.Schools.Add(school);
            }
            else
            {
                // Update School
                School dbEntry = Schools.FirstOrDefault(s => s.SchoolId == school.SchoolId);
                if(dbEntry != null)
                {
                    dbEntry.SchoolName = school.SchoolName;
                    dbEntry.InstitutionNumber = school.InstitutionNumber;
                }
            }
            _context.SaveChanges();
        }
        public School DeleteSchool(int schoolId)
        {
            School dbEntry = Schools.FirstOrDefault(s => s.SchoolId == schoolId);
            if(dbEntry != null)
            {
                _context.Schools.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        
    }
}
