using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{
    public class EFClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext context;
        public EFClassRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }


        public IEnumerable<Class> Classes => context.Classes.Include(c => c.People);

        public void SaveClass(Class theClass)
        {
            if(theClass.ClassId == 0) // new
            {
                context.Add(theClass);
            }
            else // edit
            {

            }
            context.SaveChanges();
        }
        public Class DeleteClass(int classId)
        {
            Class classToBeDeleted = Classes.FirstOrDefault(c => c.ClassId == classId);
            if(classToBeDeleted != null)
            {
                context.Remove(classToBeDeleted);
            }
            context.SaveChanges();
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
                context.SaveChanges();
            }
        }
    }
}
