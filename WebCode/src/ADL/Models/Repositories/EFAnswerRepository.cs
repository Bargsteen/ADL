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
            //SaveAnswer(new Answer()
            //{
            //    UserId = "ed88cf36-3a7e-4131-a503-b2e2fee0245d",
            //    AnsweredAssignmentId = 5,
            //    ChosenAnswer = 2,
            //    TimeAnswered = DateTime.Now,
            //    Type = EnumCollection.AssignmentType.ExclusiveChoice
            //});
            //SaveAnswer(new Answer()
            //{
            //    UserId = "29d3ccd5-46e2-4cf1-9bd9-c1256b506283",
            //    AnsweredAssignmentId = 5,
            //    ChosenAnswer = 1,
            //    TimeAnswered = DateTime.Now,
            //    Type = EnumCollection.AssignmentType.ExclusiveChoice
            //});
            //SaveAnswer(new Answer()
            //{
            //    UserId = "ed88cf36-3a7e-4131-a503-b2e2fee0245d",
            //    AnsweredAssignmentId = 6,
            //    ChosenAnswers = new List<AnswerBool>()
            //    {
            //        new AnswerBool() {Value = true},
            //        new AnswerBool() {Value = true},
            //        new AnswerBool() {Value = true},
            //        new AnswerBool() {Value = true}
            //    },
            //    TimeAnswered = DateTime.Now,
            //    Type = EnumCollection.AssignmentType.MultipleChoice
            //});
            //SaveAnswer(new Answer()
            //{
            //    UserId = "29d3ccd5-46e2-4cf1-9bd9-c1256b506283",
            //    AnsweredAssignmentId = 6,
            //    ChosenAnswers = new List<AnswerBool>()
            //    {
            //        new AnswerBool() {Value = true},
            //        new AnswerBool() {Value = false},
            //        new AnswerBool() {Value = true},
            //        new AnswerBool() {Value = true}
            //    },
            //    TimeAnswered = DateTime.Now,
            //    Type = EnumCollection.AssignmentType.MultipleChoice
            //});
            //SaveAnswer(new Answer()
            //{
            //    UserId = "ed88cf36-3a7e-4131-a503-b2e2fee0245d",
            //    AnsweredAssignmentId = 4,
            //    AnswerText = "100 planeter",
            //    TimeAnswered = DateTime.Now,
            //    Type = EnumCollection.AssignmentType.Text
            //});
            //SaveAnswer(new Answer()
            //{
            //    UserId = "29d3ccd5-46e2-4cf1-9bd9-c1256b506283",
            //    AnsweredAssignmentId = 4,
            //    AnswerText = "5 planeter",
            //    TimeAnswered = DateTime.Now,
            //    Type = EnumCollection.AssignmentType.Text
            //});
        }

        public IEnumerable<Answer> Answers => context.Answers.Include(a => a.ChosenAnswers);

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



