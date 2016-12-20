using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{

    public class EfAssignmentSetRepository : IAssignmentSetRepository
    {
        private readonly ApplicationDbContext _context;

        public EfAssignmentSetRepository(ApplicationDbContext ctx)

        {
            _context = ctx;
        }

        public IEnumerable<AssignmentSet> AssignmentSets => _context.AssignmentSets
            .Include(aS => aS.Assignments)
                .ThenInclude(a => a.AnswerOptions)
            .Include(aS => aS.Assignments)
                .ThenInclude(a => a.AnswerCorrectness);

        public void SaveAssignmentSet(AssignmentSet assignmentSet)
        {
            if (assignmentSet.AssignmentSetId == 0) // new AssignmentSet
            {
                _context.AssignmentSets.Add(assignmentSet);

            }
            else // Ís currently not in use, because there are missing elements in JS
            {
                AssignmentSet dbEntrySet = AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSet.AssignmentSetId);
                dbEntrySet.Title = assignmentSet.Title;
                dbEntrySet.Description = assignmentSet.Description;
                dbEntrySet.PublicityLevel = assignmentSet.PublicityLevel;
                dbEntrySet.LastUpdateDate = assignmentSet.LastUpdateDate;

                _context.RemoveRange(dbEntrySet.Assignments);
                // probably doesn't handle answerOptions 
                _context.AddRange(assignmentSet.Assignments);
            }

            _context.SaveChanges();
        }

        public AssignmentSet DeleteAssignmentSet(int assignmentSetId)
        {
            AssignmentSet dbEntry = AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSetId);
            if (dbEntry != null)
            {
                _context.RemoveRange(dbEntry.Assignments);
                var allAnswerOptionsInSet = dbEntry.Assignments.SelectMany(a => a.AnswerOptions);
                var allAnswerBoolsInSet = dbEntry.Assignments.SelectMany(a => a.AnswerCorrectness);
                foreach(var answerOption in allAnswerOptionsInSet)
                {
                    _context.AnswerOptions.Remove(answerOption);
                }
                foreach(var answerBool in allAnswerBoolsInSet)
                {
                    _context.AnswerBools.Remove(answerBool);
                }
                _context.AssignmentSets.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
