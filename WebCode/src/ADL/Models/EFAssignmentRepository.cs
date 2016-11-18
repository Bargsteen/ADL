using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models {

    public class EFAssignmentRepository : IAssignmentRepository 
    {
        private ApplicationDbContext context;

        public EFAssignmentRepository(ApplicationDbContext ctx) 
        {
            context = ctx;
        }

        public IEnumerable<Assignment> Assignments => context.Assignments.Include(a => a.AnswerOptions);

        public void SaveAssignment(Assignment assignment)
        {
            context.AttachRange(assignment.AnswerOptions);
            if(assignment.AssignmentId == 0)
            {
                // New assignment
                context.Add(assignment);
            }
            else
            {
                // Update already existing assignment
                Assignment dbEntry = context.Assignments.FirstOrDefault(a => a.AssignmentId == assignment.AssignmentId);
                if(dbEntry != null)
                {
                    dbEntry.Headline = assignment.Headline;
                    dbEntry.Question = assignment.Question;
                }
            }
            context.SaveChanges();
        }
        
        public Assignment DeleteAssignment(int assignmentId)
        {
            Assignment dbEntry = context.Assignments.FirstOrDefault(a => a.AssignmentId == assignmentId);
            if(dbEntry != null)
            {
                context.Assignments.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
