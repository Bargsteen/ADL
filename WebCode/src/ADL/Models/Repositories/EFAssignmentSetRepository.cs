using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static ADL.Models.EnumCollection;
using ADL.Models.Assignments;

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
            .Include(a => a.Assignments.Where(aa => aa.Type == AssignmentType.ExclusiveChoice || aa.Type == AssignmentType.MultipleChoice))
            .ThenInclude(b => (b as ExclusiveChoiceAssignment).AnswerOptions != null || (b as MultipleChoiceAssignment).AnswerOptions != null)
            .Include(c => c.Assignments.Where(cc => cc.Type == AssignmentType.ExclusiveChoice));
            

        public void SaveAssignmentSet(AssignmentSet assignmentSet)
        {
            if (assignmentSet.AssignmentSetId == 0) // new AssignmentSet
            {
                foreach (Assignment assignment in assignmentSet.Assignments)
                {
                    if (assignment.Type == AssignmentType.ExclusiveChoice)
                    {
                        (assignment as ExclusiveChoiceAssignment).AnswerOptions = (assignment as ExclusiveChoiceAssignment).AnswerOptions.Where(ao => ao.Text != null).ToList();
                        assignmentSet.Assignments.Add(assignment);
                    }
                    else if (assignment.Type == AssignmentType.MultipleChoice)
                    {
                        (assignment as MultipleChoiceAssignment).AnswerOptions = (assignment as MultipleChoiceAssignment).AnswerOptions.Where(ao => ao.Text != null).ToList();
                        assignmentSet.Assignments.Add(assignment);
                    }
                    else if(assignment.Type == AssignmentType.Text)
                    {
                        assignmentSet.Assignments.Add(assignment);
                    }
                }
                
                context.Add(assignmentSet);
                
            }
            else // Updating
            {
                AssignmentSet dbEntrySet = context.AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSet.AssignmentSetId);
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
                // probably missing the answerOptions
                context.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
