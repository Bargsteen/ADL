using System;
using System.Collections.Generic;
using System.Linq;
using ADL.Models.Answers;

namespace ADL.Models.Repositories
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
            context.Add(answer);
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



