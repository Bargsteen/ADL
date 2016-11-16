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
            if(assignment.AssignmentId == 0)
            {
                // New assignment
                context.Add(assignment);
            }
            else
            {
                Assignment dbEntry = context.Assignments.FirstOrDefault(a => a.AssignmentId == assignment.AssignmentId);
                if(dbEntry != null)
                {
                    dbEntry.Headline = assignment.Headline;    
                }
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
