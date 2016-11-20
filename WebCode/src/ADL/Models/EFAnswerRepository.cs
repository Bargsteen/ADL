using System;
using System.Collections.Generic;
using System.Linq;

namespace ADL.Models
{

    public class EFAnswerRepository : IAnswerRepository
    {
        private ApplicationDbContext context;

        public EFAnswerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Answer> Answers => context.Answers;

        public void SaveAnswer(Answer answer)
        {
            if (answer.AnswerId == 0)
            {
                // This is a new answer
                context.Answers.Add(answer);
            }
            else
            {
                // Editing an existing answer
                Answer dbEntry = context.Answers.FirstOrDefault(l => l.AnswerId == answer.AnswerId);
                if (dbEntry != null)
                {
                    dbEntry.ChosenAnswerOption = answer.ChosenAnswerOption;
                    dbEntry.TimeAnswered = answer.TimeAnswered;
                }
            }
            context.SaveChanges();
        }

        public Answer DeleteAnswer(int answerId)
        {
            Answer dbEntry = context.Answers
                .FirstOrDefault(l => l.AnswerId == answerId);
            if (dbEntry != null)
            {
                context.Answers.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}



