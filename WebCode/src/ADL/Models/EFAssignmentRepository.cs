using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models
{

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
            if (assignment.AssignmentId == 0)
            {
                // New assignment
                assignment.AnswerOptions = assignment.AnswerOptions.Where(ao => ao.Text != null).ToList();
                context.Add(assignment);
            }
            else
            {
                // Update already existing assignment
                Assignment dbEntry = context.Assignments
                    .Include(a => a.AnswerOptions)
                    .FirstOrDefault(a => a.AssignmentId == assignment.AssignmentId);

                if (dbEntry != null)
                {
                    dbEntry.Headline = assignment.Headline;
                    dbEntry.Question = assignment.Question;
                    int amountOfAnswerOptionsInDb = dbEntry.AnswerOptions.Count();
                    foreach(AnswerOption ao in dbEntry.AnswerOptions)
                    {
                        context.Remove(ao);
                    }
                    dbEntry.AnswerOptions = new List<AnswerOption>();
                    foreach (AnswerOption ao in assignment.AnswerOptions)
                    {
                        if(ao.Text != null)
                        {
                            dbEntry.AnswerOptions.Add(ao);
                        }
                    }
                }
            }
            context.SaveChanges();
        }

        public Assignment DeleteAssignment(int assignmentId)
        {
            Assignment dbEntry = context.Assignments.Include(a => a.AnswerOptions).FirstOrDefault(a => a.AssignmentId == assignmentId);
            if (dbEntry != null)
            {
                context.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
