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
                // Update already existing assignment
                Assignment dbEntry = context.Assignments.FirstOrDefault(a => a.AssignmentId == assignment.AssignmentId);
                if(dbEntry != null)
                {
                    dbEntry.Headline = assignment.Headline;
                    dbEntry.Question = assignment.Question;
                    dbEntry.AnswerOptionOne = assignment.AnswerOptionOne;
                    dbEntry.AnswerOptionTwo = assignment.AnswerOptionTwo;
                    dbEntry.AnswerOptionThree = assignment.AnswerOptionThree;
                    dbEntry.AnswerOptionFour = assignment.AnswerOptionFour;
                    dbEntry.CorrectAnswer = assignment.CorrectAnswer;
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
