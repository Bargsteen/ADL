using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ADL.Models.Answers;
using ADL.Models.Assignments;
using ADL.Models.Repositories;

namespace ADL.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public IQueryable<Person> People { get; set; }
        public IEnumerable<AssignmentSet> AssignmentSets { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Class> ClassesInSchool { get; set; }
        public Dictionary<int, Tuple<int, int,int>> CorrectVsTotalForSet { get; set; } = new Dictionary<int, Tuple<int, int,int>>();
        public List<AnswerInformationPerSetViewModel> AnswerInformationViewModels { get; set; } = new List<AnswerInformationPerSetViewModel>();

        private IEnumerable<Answer> GetAnswersForAssignment(Assignment assignment, IEnumerable<Answer> answers)
        {
            List<Answer> answersForThisAssignment = new List<Answer>();
            foreach (Answer answer in answers)
            {
                if (answer.AnsweredAssignmentId == assignment.AssignmentId)
                {
                    answersForThisAssignment.Add(answer);
                }
            }
            return answersForThisAssignment;
        }

        private Tuple<int, int, int> GetCorrectVsTotalForSet(AssignmentSet assignmentSet)
        {
            int correctAnswers = 0;
            int totalAnswers = 0;
            int answersForReview = 0;
            foreach (Assignment assignment in assignmentSet.Assignments)
            {
                List<Answer> answersForThisAssignment = GetAnswersForAssignment(assignment,
                    Answers) as List<Answer>;
                if (assignment.Type == EnumCollection.AssignmentType.ExclusiveChoice)
                {

                    correctAnswers += answersForThisAssignment.Count(a => a.ChosenAnswer == assignment.CorrectAnswer);
                    totalAnswers += answersForThisAssignment.Count();
                }
                else if (assignment.Type == EnumCollection.AssignmentType.MultipleChoice)
                {
                    foreach (Answer a in answersForThisAssignment)
                    {
                        int mCorrectAnswers = 0;
                        int answerOptionCounter = 0;
                        foreach (AnswerBool b in assignment.AnswerCorrectness)
                        {
                            if (a.ChosenAnswers[answerOptionCounter++].Value == b.Value)
                                mCorrectAnswers++;
                        }
                        if (mCorrectAnswers == assignment.AnswerOptions.Count)
                        {
                            correctAnswers++;
                        }
                    }
                    totalAnswers += answersForThisAssignment.Count;
                }
                else
                {
                    answersForReview += answersForThisAssignment.Count();
                }

            }
            return new Tuple<int, int, int>(correctAnswers, totalAnswers, answersForReview);
        }

        public StatisticsViewModel(IEnumerable<Answer> answerRepositoryAnswers, IEnumerable<AssignmentSet> assignmentSets, IQueryable<Person> people, Person currentUser, IEnumerable<Class> classesInSchool)
        {
            ClassesInSchool = classesInSchool;
            Answers = answerRepositoryAnswers;
            AssignmentSets = assignmentSets;
            People = people;
            foreach (AssignmentSet assignmentSet in AssignmentSets.Where(set => set.CreatorId == currentUser.Id))
            {
                foreach (Person person in People)
                {
                    AnswerInformationViewModels.Add(new AnswerInformationPerSetViewModel(person, assignmentSet, Answers));
                }
                CorrectVsTotalForSet.Add(assignmentSet.AssignmentSetId,GetCorrectVsTotalForSet(assignmentSet));
            }
            
        }
    }
    public class AnswerInformationPerSetViewModel
    {
        private static int _idCounter = 0;
        public int CollapseId { get; set; }
        public Person User { get; set; }
        public AssignmentSet AssignmentSet { get; set; }
        public List<Tuple<Assignment, Answer>> AssignmentAnswers { get; set; } = new List<Tuple<Assignment, Answer>>();

        public AnswerInformationPerSetViewModel(Person user, AssignmentSet set, IEnumerable<Answer> answers)
        {
            CollapseId = _idCounter++;
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