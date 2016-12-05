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
        IAssignmentSetRepository assignmentSetRepository;
        ILocationRepository locationRepository;
        UserManager<Person> userManager;

        public AssignmentController(IAssignmentSetRepository assignmentSetRepo, ILocationRepository locationRepo, UserManager<Person> usrMgr)
        {
            assignmentSetRepository = assignmentSetRepo;
            locationRepository = locationRepo;
            userManager = usrMgr;
        }

        public async Task<ViewResult> AssignmentSetList()
        {
            Person currentUser = await GetCurrentUserAsync();
            AssignmentSetListViewModel model = new AssignmentSetListViewModel()
            {
                PublicAssignmentSets = assignmentSetRepository.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Public),
                InternalAssignmentSets = assignmentSetRepository.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Internal && a.SchoolId == currentUser.SchoolId),
                PrivateAssignmentSets = assignmentSetRepository.AssignmentSets.Where(a => a.PublicityLevel == PublicityLevel.Private && a.CreatorId == currentUser.Id)

            };
            return View(model);
        }

        public ViewResult Edit(int assignmentSetId)
        {
            AssignmentSet assignmentSet = assignmentSetRepository.AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSetId);
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
                    foreach (TextAssignment tA in model.TextAssignments)
                    {
                        model.AssignmentSet.Assignments.Add(tA);
                    }
                }
                if (model.ExclusiveChoiceAssignments != null)
                {
                    foreach (ExclusiveChoiceAssignment ecA in model.ExclusiveChoiceAssignments)
                    {
                        model.AssignmentSet.Assignments.Add(ecA);
                    }
                }
                if (model.MultipleChoiceAssignments != null)
                {
                    foreach (MultipleChoiceAssignment mcA in model.MultipleChoiceAssignments)
                    {
                        model.AssignmentSet.Assignments.Add(mcA);
                    }

                }
                if (model.AssignmentSet.Assignments.Count > 0)
                {
                    assignmentSetRepository.SaveAssignmentSet(model.AssignmentSet);
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
            Person currentUser = await GetCurrentUserAsync();

            AssignmentSet assignmentSet = new AssignmentSet()
            {
                CreatorId = currentUser.Id,
            };
            AssignmentSetViewModel model = new AssignmentSetViewModel()
            {
                AssignmentSet = assignmentSet
            };
            return View(nameof(Edit), model);
        }


        public Task<Person> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        public IActionResult DeleteAssignmentSet(int assignmentSetId)
        {
            AssignmentSet deletedAssignmentSet = assignmentSetRepository.DeleteAssignmentSet(assignmentSetId);
            if (deletedAssignmentSet != null)
            {
                TempData["message"] = $"Opgaven '{deletedAssignmentSet.Title}' blev slettet.";
            }
            return RedirectToAction(nameof(AssignmentSetList));
        }
    }
}