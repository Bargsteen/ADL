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

            SaveAnswer(new Answer()
            {
                AnsweredAssignmentId = 2, ChosenAnswer = 0, TimeAnswered = DateTime.Now, Type = EnumCollection.AssignmentType.ExclusiveChoice, UserId = "4d783701-c83a-4380-ac1b-1ea7dbb2e8d0"
            });
            SaveAnswer(new Answer()
            {
                AnsweredAssignmentId = 1, ChosenAnswers = new List<AnswerBool>() {
                    new AnswerBool()
                {
                    Value = true
                }, new AnswerBool()
                {
                    Value = false
                }, new AnswerBool()
                {
                    Value = true
                } }, TimeAnswered = DateTime.Now, UserId = "4d783701-c83a-4380-ac1b-1ea7dbb2e8d0",
                Type = EnumCollection.AssignmentType.MultipleChoice

            });
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



