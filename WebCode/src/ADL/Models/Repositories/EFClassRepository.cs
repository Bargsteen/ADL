using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{
    public class EfClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;
        public EfClassRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }


        public IEnumerable<Class> Classes => _context.Classes.Include(c => c.People);

        public void SaveClass(Class theClass)
        {
            if(theClass.ClassId == 0) // new
            {
                _context.Classes.Add(theClass);
            }
            _context.SaveChanges();
        }
        public Class DeleteClass(int classId)
        {
            Class classToBeDeleted = Classes.FirstOrDefault(c => c.ClassId == classId);
            if(classToBeDeleted != null)
            {
                _context.Classes.Remove(classToBeDeleted);
            }
            _context.SaveChanges();
            return classToBeDeleted;
        }

        public void AddPersonToClass(int classId, Person newPerson)
        {
            Class dbEntry = Classes.FirstOrDefault(c => c.ClassId == classId);
            if(dbEntry != null)
            {
                if(dbEntry.People == null)
                {
                    dbEntry.People = new List<Person>();
                }
                dbEntry.People.Add(newPerson);
                _context.SaveChanges();
            }
        }
    }
}
