using Microsoft.AspNetCore.Mvc;
using System;
using ADL.Models;
using ADL.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ADL.Controllers
{
    public class StatisticsController : Controller
    {
        private IAnswerRepository answerRepository;
        private IAssignmentRepository assignmentRepository;
        public StatisticsController(IAssignmentRepository assignmentRepo, IAnswerRepository answerRepo)
        {
            answerRepository = answerRepo;
            assignmentRepository = assignmentRepo;
        }
        public ViewResult Index()
        {
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel() 
            { 
                Answers = answerRepository.Answers, 
                Assignments = assignmentRepository.Assignments 
            };
            
            Dictionary<int, Tuple<int, int>> statsList = new Dictionary<int, Tuple<int, int>>();

            foreach(Assignment assignment in statisticsViewModel.Assignments)
            {
                List<Answer> answersForThisAssignment = new List<Answer>();
                foreach(Answer answer in statisticsViewModel.Answers)
                {
                    if(answer.AnsweredAssignmentId == assignment.AssignmentId)
                    {
                        answersForThisAssignment.Add(answer);
                    }
                }
                Tuple<int, int> correctVsTotalAnswers = new Tuple<int, int>
                (
                    answersForThisAssignment.Where(a => a.ChosenAnswerOption == assignment.CorrectAnswer).Count(), // Count # of correct answers
                    answersForThisAssignment.Count()
                );
                statsList.Add(assignment.AssignmentId, correctVsTotalAnswers);
            }
            statisticsViewModel.Stats = statsList;

            return View(statisticsViewModel);
        }
    }
}