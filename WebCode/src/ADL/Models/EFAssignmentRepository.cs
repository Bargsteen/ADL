using System;
using System.Collections.Generic;
using System.Linq;

namespace ADL.Models {

    public class EFAssignmentRepository : IAssignmentRepository 
    {
        private ApplicationDbContext context;

        public EFAssignmentRepository(ApplicationDbContext ctx) 
        {
            context = ctx;
        }

        public IEnumerable<Assignment> Assignments => context.Assignments;

        public void Add(Assignment assignment)
        {
            context.Add(assignment);
            context.SaveChanges();
        }
        public void Edit(Assignment editedAssignment)
        {
            context.Update(editedAssignment);
            context.SaveChanges();
        }

        
        public void Delete(Assignment assignment)
        {
            context.Remove(assignment);
            context.SaveChanges();
        }
    }
}
