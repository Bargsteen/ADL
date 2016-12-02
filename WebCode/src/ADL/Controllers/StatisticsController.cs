using Microsoft.AspNetCore.Mvc;
using System;
using ADL.Models;
using ADL.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer")]
    public class StatisticsController : Controller
    {
        private IAnswerRepository answerRepository;
        private IAssignmentSetRepository assignmentSetRepository;
        public StatisticsController(IAssignmentSetRepository assignmentSetRepo, IAnswerRepository answerRepo)
        {
            answerRepository = answerRepo;
            assignmentSetRepository = assignmentSetRepo;
        }
        //public ViewResult Index()
        //{
        //    List<Assignment> totalList = new List<Assignment>();
        //    foreach (List<Assignment> assignmentList in assignmentSetRepository.AssignmentSets.Select(set => set.Assignments))
        //    {
        //        foreach (Assignment a in assignmentList)
        //        {
        //            totalList.Add(a);
        //        }
        //    }
        //    IEnumerable<Assignment> asi = totalList;
        //    StatisticsViewModel statisticsViewModel = new StatisticsViewModel()
        //    {
        //        Answers = answerRepository.Answers,
        //        Assignments = asi,
        //    };

        //    Dictionary<int, Tuple<int, int>> exclusiveStatsList = new Dictionary<int, Tuple<int, int>>();

        //    foreach (Assignment assignment in statisticsViewModel.Assignments)
        //    {
        //        List<Answer> answersForThisAssignment = new List<Answer>();
        //        foreach (Answer answer in statisticsViewModel.Answers)
        //        {
        //            if (answer.AnsweredAssignmentId == assignment.AssignmentId)
        //            {
        //                answersForThisAssignment.Add(answer);
        //            }
        //        }
        //        if (assignment is ExclusiveChoiceAssignment)
        //        {
        //            Tuple<int, int> correctVsTotalAnswers = new Tuple<int, int>
        //        (

        //            answersForThisAssignment.Where(a => a.ChosenAnswerOption == assignment.CorrectAnswer).Count(), // Count # of correct answers
        //            answersForThisAssignment.Count()
        //        );
        //            exclusiveStatsList.Add(assignment.AssignmentId, correctVsTotalAnswers);
        //        }

        //    }
        //    statisticsViewModel.Stats = exclusiveStatsList;

        //    return View(statisticsViewModel);
        //}
    }
}