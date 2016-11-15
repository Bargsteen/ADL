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

        public void SaveAssignment(Assignment assignment)
        {
            if(assignment.AssignmentID == 0)
            {
                // New assignment
                context.Add(assignment);
            }
            else
            {
                context.Update(assignment);
            }
            context.SaveChanges();
        }
        
        public void DeleteAssignment(Assignment assignment)
        {
            context.Remove(assignment);
            context.SaveChanges();
        }
    }
}
