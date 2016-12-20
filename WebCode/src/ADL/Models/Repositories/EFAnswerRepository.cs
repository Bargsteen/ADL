using System.Collections.Generic;
using ADL.Models.Answers;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{

    public class EfAnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public EfAnswerRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Answer> Answers => _context.Answers.Include(a => a.ChosenAnswers);

        public void SaveAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }
    }
}



