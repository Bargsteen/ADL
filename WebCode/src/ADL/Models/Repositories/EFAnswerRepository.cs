using System;
using System.Collections.Generic;
using System.Linq;
using ADL.Models.Answers;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{

    public class EFAnswerRepository : IAnswerRepository
    {
        private ApplicationDbContext context;

        public EFAnswerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Answer> Answers => context.Answers.Include(a => a.ChosenAnswers);

        public void SaveAnswer(Answer answer)
        {
            context.Add(answer);
            context.SaveChanges();
        }

        public Answer DeleteAnswer(int answerId)
        {
            Answer dbEntry = Answers.FirstOrDefault(l => l.AnswerId == answerId);
            if (dbEntry != null)
            {
                context.Answers.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}



