using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{

    public class EFAssignmentSetRepository : IAssignmentSetRepository
    {
        private ApplicationDbContext context;

        public EFAssignmentSetRepository(ApplicationDbContext ctx)

        {
            context = ctx;
        }

        public IEnumerable<AssignmentSet> AssignmentSets => context.AssignmentSets
            .Include(aS => aS.Assignments)
                .ThenInclude(a => a.AnswerOptions)
            .Include(aS => aS.Assignments)
                .ThenInclude(a => a.AnswerCorrectness);

        public void SaveAssignmentSet(AssignmentSet assignmentSet)
        {
            if (assignmentSet.AssignmentSetId == 0) // new AssignmentSet
            {
                context.AssignmentSets.Add(assignmentSet);

            }
            else // Updating
            {
                AssignmentSet dbEntrySet = AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSet.AssignmentSetId);
                dbEntrySet.Title = assignmentSet.Title;
                dbEntrySet.Description = assignmentSet.Description;
                dbEntrySet.PublicityLevel = assignmentSet.PublicityLevel;
                dbEntrySet.LastUpdateDate = assignmentSet.LastUpdateDate;

                context.RemoveRange(dbEntrySet.Assignments);
                // probably doesn't handle answerOptions 
                context.AddRange(assignmentSet.Assignments);
            }

            context.SaveChanges();
        }

        public AssignmentSet DeleteAssignmentSet(int assignmentSetId)
        {
            AssignmentSet dbEntry = AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSetId);
            if (dbEntry != null)
            {
                context.RemoveRange(dbEntry.Assignments);
                var allAnswerOptionsInSet = dbEntry.Assignments.SelectMany(a => a.AnswerOptions);
                var allAnswerBoolsInSet = dbEntry.Assignments.SelectMany(a => a.AnswerCorrectness);
                foreach(var answerOption in allAnswerOptionsInSet)
                {
                    context.AnswerOptions.Remove(answerOption);
                }
                foreach(var answerBool in allAnswerBoolsInSet)
                {
                    context.AnswerBools.Remove(answerBool);
                }
                context.AssignmentSets.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
