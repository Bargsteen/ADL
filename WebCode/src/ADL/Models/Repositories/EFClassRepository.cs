using System;
using System.Collections.Generic;
using System.Linq;

namespace ADL.Models.Repositories
{
    public class EFClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext context;
        public EFClassRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }


        public IEnumerable<Class> Classes => context.Classes;

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
            Class classToBeDeleted = context.Classes.FirstOrDefault(c => c.ClassId == classId);
            if(classToBeDeleted != null)
            {
                context.Remove(classToBeDeleted);
            }
            context.SaveChanges();
            return classToBeDeleted;
        }
    }
}
