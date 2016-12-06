using System;
using System.Collections.Generic;
using System.Linq;
using ADL.Models.Answers;
using ADL.Models.Assignments;

namespace ADL.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public IQueryable<Person> People { get; set; }
        public IEnumerable<AssignmentSet> AssignmentSets { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public Dictionary<int, Tuple<int, int>> CorrectVsTotalForExclusiveAssignments { get; set; }
        public Dictionary<int, IEnumerable<Tuple<string, double>>> CorrectPercentagesForMultipleAssignments { get; set; }
        public Dictionary<int, IEnumerable<Tuple<string, string>>> TextualAnswersForAssignments { get; set; }
        public List<AnswerInformationPerSetViewModel> AnswerInformationViewModels { get; set; }

        public StatisticsViewModel(IEnumerable<Answer> answerRepositoryAnswers, IEnumerable<AssignmentSet> assignmentSets, IQueryable<Person> people)
        {
            Answers = answerRepositoryAnswers;
            AssignmentSets = assignmentSets;
            People = people;
            //foreach (AssignmentSet assignmentSet in AssignmentSets)
            //{
            //    foreach (Person person in People)
            //    {
            //        AnswerInformationViewModels.Add(new AnswerInformationPerSetViewModel(person, assignmentSet, Answers));
            //    }
            //}
        }
    }
    public class AnswerInformationPerSetViewModel
    {
        public Person User { get; set; }
        public AssignmentSet AssignmentSet { get; set; }
        public List<Tuple<Assignment, Answer>> AssignmentAnswers { get; set; }

        public AnswerInformationPerSetViewModel(Person user, AssignmentSet set, IEnumerable<Answer> answers)
        {
            User = user;
            AssignmentSet = set;
            foreach (Answer answer in answers.Where(a => a.UserId == User.Id))
            {
                foreach (Assignment assignment in AssignmentSet.Assignments.Where(ass => ass.AssignmentId == answer.AnsweredAssignmentId))
                {
                    AssignmentAnswers.Add(new Tuple<Assignment, Answer>(assignment, answer));
                }

            }
        }
    }
}