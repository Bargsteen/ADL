using Microsoft.AspNetCore.Mvc;
using System;
using ADL.Models;
using ADL.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer,Admin")]
    public class StatisticsController : Controller
    {
        private IAnswerRepository answerRepository;
        private IAssignmentSetRepository assignmentSetRepository;
        public StatisticsController(IAssignmentSetRepository assignmentSetRepo, IAnswerRepository answerRepo)
        {
            answerRepository = answerRepo;
            assignmentSetRepository = assignmentSetRepo;
        }

        private IEnumerable<Answer> getAnswersForAssignment(Assignment assignment, IEnumerable<Answer> answers)
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
        private void AddExclusiveStats(ExclusiveChoiceAssignment assignment, Dictionary<int, Tuple<int, int>> exclusiveStatsList)
        {
            List<Answer> answersForThisAssignment = getAnswersForAssignment(assignment,
                answerRepository.Answers) as List<Answer>;
            Tuple<int, int> correctVsTotalAnswers =
                new Tuple<int, int>(answersForThisAssignment.Count(a => int.Parse(a.ChosenAnswers[0]) == assignment.CorrectAnswer), answersForThisAssignment.Count);
            exclusiveStatsList.Add(assignment.AssignmentId, correctVsTotalAnswers);
        }

        private void AddMultipleStats(MultipleChoiceAssignment assignment, Dictionary<int, IEnumerable<double>> mStatsList)
        {
            List<double> answerPcts = new List<double>();
            List<Answer> answersForThisAssignment = getAnswersForAssignment(assignment, answerRepository.Answers) as List<Answer>;
            foreach (Answer a in answersForThisAssignment)
            {
                int correctAnswers = 0;
                foreach (int correctAnswerIndex in assignment.CorrectAnswers)
                {
                    if (a.ChosenAnswers.Exists(ca => int.Parse(ca) == correctAnswerIndex))
                        correctAnswers++;
                }
                answerPcts.Add((double)correctAnswers / assignment.AnswerOptions.Count);
            }
            mStatsList.Add(assignment.AssignmentId, answerPcts);
        }

        private IEnumerable<string> addTextualAnswers(Assignment assignment)
        {
            List<Answer> answersForThisAssignment = getAnswersForAssignment(assignment, answerRepository.Answers) as List<Answer>;
            return answersForThisAssignment.Select(a => a.ChosenAnswers[0]).ToList();
        }

        public ViewResult Index()
        {
            List<Assignment> totalList = new List<Assignment>();
            foreach (List<Assignment> assignmentList in assignmentSetRepository.AssignmentSets.Select(set => set.Assignments))
            {
                foreach (Assignment a in assignmentList)
                {
                    totalList.Add(a);
                }
            }
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel()
            {
                Answers = answerRepository.Answers,
                AssignmentSets = assignmentSetRepository.AssignmentSets
            };
            foreach (Assignment assignment in totalList)
            {
                if (assignment is ExclusiveChoiceAssignment)
                {
                    Dictionary<int, Tuple<int, int>> exclusiveStatsList = new Dictionary<int, Tuple<int, int>>();
                    AddExclusiveStats(assignment as ExclusiveChoiceAssignment, exclusiveStatsList);
                    statisticsViewModel.ExclusiveStats = exclusiveStatsList;
                }

                else if (assignment is MultipleChoiceAssignment)
                {
                    Dictionary<int, IEnumerable<double>> mStatsList = new Dictionary<int, IEnumerable<double>>();
                    AddMultipleStats(assignment as MultipleChoiceAssignment, mStatsList);
                    statisticsViewModel.MultipleStats = mStatsList;
                }

                else
                {
                    statisticsViewModel.TextualAnswers.Add(assignment.AssignmentId, addTextualAnswers(assignment));
                }
            }
            return View(statisticsViewModel);
        }
    }
}