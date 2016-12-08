using Microsoft.AspNetCore.Mvc;
using System;
using ADL.Models;
using ADL.Models.Repositories;
using ADL.Models.Assignments;
using ADL.Models.Answers;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IClassRepository _classRepository;

        public StatisticsController(IAssignmentSetRepository assignmentSetRepo, IAnswerRepository answerRepo, UserManager<Person> userManager, IClassRepository classRepository)
        {
            _answerRepository = answerRepo;
            _userManager = userManager;
            _classRepository = classRepository;
            _assignmentSetRepository = assignmentSetRepo;
        }

        public async Task<ViewResult> Index()
        {
            Person currentUser = await _userManager.GetUserAsync(HttpContext.User);
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel(_answerRepository.Answers,
                _assignmentSetRepository.AssignmentSets,
                _userManager.Users.Where(
                    p => p.PersonType == EnumCollection.PersonTypes.Student && p.SchoolId == currentUser.SchoolId), currentUser, _classRepository.Classes.Where(c => c.SchoolId == currentUser.SchoolId));
            return View(statisticsViewModel);
        }
    }
}