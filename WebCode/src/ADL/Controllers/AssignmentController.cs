using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using ADL.Models.Assignments;
using ADL.Models.Repositories;
using System.Linq;
using System.Collections.Generic;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using static ADL.Models.EnumCollection;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer,Admin")]
    public class AssignmentController : Controller
    {
        readonly IAssignmentSetRepository _assignmentSetRepository;
        ILocationRepository _locationRepository;
        readonly UserManager<Person> _userManager;
        Person _currentUser;
        public AssignmentController(IAssignmentSetRepository assignmentSetRepo, ILocationRepository locationRepo, UserManager<Person> usrMgr)
        {
            _assignmentSetRepository = assignmentSetRepo;
            _locationRepository = locationRepo;
            _userManager = usrMgr;
        }

        public async Task<ViewResult> AssignmentSetList()
        {
            if (_currentUser == null)
            {
                _currentUser = await GetCurrentUserAsync();
            }
            AssignmentSetListViewModel model = new AssignmentSetListViewModel()
            {
                PublicAssignmentSets = _assignmentSetRepository.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Public),
                InternalAssignmentSets = _assignmentSetRepository.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Internal && a.SchoolId == _currentUser.SchoolId),
                PrivateAssignmentSets = _assignmentSetRepository.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Private && a.CreatorId == _currentUser.Id),
                CurrentSchoolId = _currentUser.SchoolId
            };
            return View(model);
        }

        public ViewResult Edit(int assignmentSetId)
        {
            AssignmentSet assignmentSet = _assignmentSetRepository.AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSetId);
            AssignmentSetViewModel model = new AssignmentSetViewModel { AssignmentSet = assignmentSet };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AssignmentSetViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.AssignmentSet.Assignments = new List<Assignment>();

                if (model.TextAssignments != null)
                {
                    foreach (Assignment tA in model.TextAssignments)
                    {
                        model.AssignmentSet.Assignments.Add(tA);
                    }
                }
                if (model.ExclusiveChoiceAssignments != null)
                {
                    foreach (Assignment ecA in model.ExclusiveChoiceAssignments)
                    {
                        model.AssignmentSet.Assignments.Add(ecA);
                    }
                }
                if (model.MultipleChoiceAssignments != null)
                {
                    foreach (Assignment mcA in model.MultipleChoiceAssignments)
                    {
                        model.AssignmentSet.Assignments.Add(mcA);
                    }

                }
                if (model.AssignmentSet.Assignments.Count > 0)
                {
                    _assignmentSetRepository.SaveAssignmentSet(model.AssignmentSet);
                    TempData["message"] = $"Opgaven '{model.AssignmentSet.Title}' blev gemt.";
                    return RedirectToAction(nameof(AssignmentSetList));
                }
                else
                { // No assignments were created.
                    ModelState.AddModelError("", "Der skal være tilføjet mindst én opgave.");
                }
            }
            // Something was wrong with the entered data
            return View(model);
        }

        // Uses the edit view, but gives it a new assignment.
        public async Task<ViewResult> Create()
        {
            if (_currentUser == null)
            {
                _currentUser = await GetCurrentUserAsync();
            }

            AssignmentSet assignmentSet = new AssignmentSet()
            {
                CreatorId = _currentUser.Id,
                SchoolId = _currentUser.SchoolId
            };
            AssignmentSetViewModel model = new AssignmentSetViewModel()
            {
                AssignmentSet = assignmentSet
            };
            return View(nameof(Edit), model);
        }


        private Task<Person> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        public IActionResult DeleteAssignmentSet(int assignmentSetId)
        {
            AssignmentSet deletedAssignmentSet = _assignmentSetRepository.DeleteAssignmentSet(assignmentSetId);
            if (deletedAssignmentSet != null)
            {
                TempData["message"] = $"Opgavesættet '{deletedAssignmentSet.Title}' blev slettet.";
            }
            return RedirectToAction(nameof(AssignmentSetList));
        }
    }
}