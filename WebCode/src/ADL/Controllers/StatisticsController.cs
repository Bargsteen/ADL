﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IAnswerRepository _answerRepository;
        private readonly IAssignmentSetRepository _assignmentSetRepository;
        public StatisticsController(IAssignmentSetRepository assignmentSetRepo, IAnswerRepository answerRepo)
        {
            _answerRepository = answerRepo;
            _assignmentSetRepository = assignmentSetRepo;
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
        private Tuple<int, int> GetCorrectVsTotalForExclusiveAssignment(ExclusiveChoiceAssignment assignment)
        {
            List<Answer> answersForThisAssignment = getAnswersForAssignment(assignment,
                _answerRepository.Answers) as List<Answer>;
            Tuple<int, int> correctVsTotalAnswers =
                new Tuple<int, int>(answersForThisAssignment.Count(a => int.Parse(a.ChosenAnswers[0]) == assignment.CorrectAnswer), answersForThisAssignment.Count);
            return correctVsTotalAnswers;
        }

        private IEnumerable<double> GetCorrectPercentageForMultipleAssignment(MultipleChoiceAssignment assignment)
        {
            List<double> correctPercentages = new List<double>();
            List<Answer> answersForThisAssignment = getAnswersForAssignment(assignment, _answerRepository.Answers) as List<Answer>;
            foreach (Answer a in answersForThisAssignment)
            {
                int correctAnswers = 0;
                foreach (int correctAnswerIndex in assignment.CorrectAnswers)
                {
                    if (a.ChosenAnswers.Exists(ca => int.Parse(ca) == correctAnswerIndex))
                        correctAnswers++;
                }
                correctPercentages.Add(((double)correctAnswers / assignment.AnswerOptions.Count) * 100);
            }
            return correctPercentages;
        }

        private IEnumerable<string> GetAnswersForTextualAssignment(Assignment assignment)
        {
            List<Answer> answersForThisAssignment = getAnswersForAssignment(assignment, _answerRepository.Answers) as List<Answer>;
            return answersForThisAssignment.Select(a => a.ChosenAnswers[0]).ToList();
        }

        public ViewResult Index()
        {
            List<Assignment> allAssignments = new List<Assignment>();
            foreach (List<Assignment> assignmentList in _assignmentSetRepository.AssignmentSets.Select(set => set.Assignments))
            {
                foreach (Assignment a in assignmentList)
                {
                    allAssignments.Add(a);
                }
            }
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel()
            {
                Answers = _answerRepository.Answers,
                AssignmentSets = _assignmentSetRepository.AssignmentSets
            };
            foreach (Assignment assignment in allAssignments)
            {
                if (assignment is ExclusiveChoiceAssignment)
                {
                    statisticsViewModel.CorrectVsTotalForExclusiveAssignments
                        .Add(assignment.AssignmentId
                             , GetCorrectVsTotalForExclusiveAssignment(assignment as ExclusiveChoiceAssignment));
                }

                else if (assignment is MultipleChoiceAssignment)
                {
                    statisticsViewModel.CorrectPercentagesForMultipleAssignments
                        .Add(assignment.AssignmentId
                             , GetCorrectPercentageForMultipleAssignment(assignment as MultipleChoiceAssignment));
                }
                else
                {
                    statisticsViewModel.TextualAnswersForAssignments
                        .Add(assignment.AssignmentId
                             , GetAnswersForTextualAssignment(assignment));
                }
            }
            return View(statisticsViewModel);
        }
    }
}