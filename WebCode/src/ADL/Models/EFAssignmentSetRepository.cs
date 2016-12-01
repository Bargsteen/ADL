using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static ADL.Models.EnumCollection;

namespace ADL.Models
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
            if (assignmentSet.AssignmentSetId == 0)
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
                    else
                    {
                        assignmentSet.Assignments.Add(assignment);
                    }
                }
                
                context.Add(assignmentSet);
                
            }
            else
            {
                AssignmentSet dbEntry = context.AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSet.AssignmentSetId);

                /*assignmentset felter UPDATE*/
                dbEntry.Title = assignmentSet.Title;
                dbEntry.Description = assignmentSet.Description;
                dbEntry.PublicityLevel = assignmentSet.PublicityLevel;
                //dbEntry.Creator = assignmentSet.Creator;
                dbEntry.DateOfCreation = assignmentSet.DateOfCreation;

                /*assignments felter UPDATE*/
                foreach (Assignment assignment in dbEntry.Assignments)
                {
                    assignmentSet.Assignments.Remove(assignment);
                }
                foreach (Assignment assignment in assignmentSet.Assignments)
                {
                    assignmentSet.Assignments.Add(assignment);
                }
                context.Add(assignmentSet);
            }

            context.SaveChanges();
        }

        public AssignmentSet DeleteAssignmentSet(int assignmentSetId)
        {
            AssignmentSet dbEntry = AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSetId);
            if (dbEntry != null)
            {
                context.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
