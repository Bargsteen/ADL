using Microsoft.AspNetCore.Mvc;
using System;
using ADL.Models;
using ADL.Models.Repositories;
using ADL.Models.Assignments;
using ADL.Models.Answers;
using System.Linq;
using System.Collections.Generic;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer,Admin")]
    public class StatisticsController : Controller
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IAssignmentSetRepository _assignmentSetRepository;
        private readonly UserManager<Person> _userManager;
        public StatisticsController(IAssignmentSetRepository assignmentSetRepo, IAnswerRepository answerRepo, UserManager<Person> userManager)
        {
            _answerRepository = answerRepo;
            _userManager = userManager;
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
            List<ExclusiveChoiceAnswer> answersForThisAssignment = getAnswersForAssignment(assignment,
                _answerRepository.Answers) as List<ExclusiveChoiceAnswer>;
            Tuple<int, int> correctVsTotalAnswers =
                new Tuple<int, int>(answersForThisAssignment.Count(a => a.ChosenAnswer == assignment.CorrectAnswer), answersForThisAssignment.Count);
            return correctVsTotalAnswers;
        }

        private List<Tuple<string, double>> GetCorrectPercentageForMultipleAssignment(MultipleChoiceAssignment assignment)
        {
            List<Tuple<string, double>> correctPercentages = new List<Tuple<string, double>>();
            List<MultipleChoiceAnswer> answersForThisAssignment = getAnswersForAssignment(assignment, _answerRepository.Answers) as List<MultipleChoiceAnswer>;
            foreach (MultipleChoiceAnswer a in answersForThisAssignment)
            {
                int correctAnswers = 0;
                int answerOptionCounter = 0;
                foreach (AnswerBool b in assignment.AnswerCorrectness)
                {
                    if (a.ChosenAnswers[answerOptionCounter++].Value == b.Value)
                        correctAnswers++;
                }
                correctPercentages.Add(new Tuple<string, double>(a.UserId, ((double)correctAnswers / assignment.AnswerOptions.Count) * 100));
            }
            return correctPercentages;
        }

        private IEnumerable<Tuple<string, string>> GetAnswersForTextualAssignment(Assignment assignment)
        {
            List<TextAnswer> answersForThisAssignment = getAnswersForAssignment(assignment, _answerRepository.Answers) as List<TextAnswer>;
            List<Tuple<string, string>> answersForTextualAssignment = new List<Tuple<string, string>>();
            foreach (TextAnswer answer in answersForThisAssignment)
            {
                answersForTextualAssignment.Add(new Tuple<string, string>(answer.UserId, answer.Text));
            }
            return answersForTextualAssignment;
        }

        public ViewResult Index()
        {
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel()
            {
                Answers = _answerRepository.Answers,
                AssignmentSets = _assignmentSetRepository.AssignmentSets,
                People = _userManager.Users
            };
            return View(statisticsViewModel);
        }
    }
}