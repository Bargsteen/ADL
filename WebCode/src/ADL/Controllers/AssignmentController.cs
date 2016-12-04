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

        public ViewResult List()
        {
            AssignmentAndLocationListViewModel assignmentList = new AssignmentAndLocationListViewModel()
            {
                AssignmentSets = assignmentSetRepository.AssignmentSets,
                Locations = locationRepository.Locations
            };
            return View(assignmentList);
        }

        public ViewResult Edit(int assignmentSetId)
        {
            return View(assignmentSetRepository.AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSetId));
        }

        [HttpPost]
        public IActionResult Edit(AssignmentSetViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (Assignment tA in model.TextAssignments)
                {
                    model.AssignmentSet.Assignments.Add(tA);
                }
                foreach (ExclusiveChoiceAssignment ecA in model.ExclusiveChoiceAssignments)
                {
                    model.AssignmentSet.Assignments.Add(ecA);
                }
                foreach (MultipleChoiceAssignment mcA in model.MultipleChoiceAssignments)
                {
                    model.AssignmentSet.Assignments.Add(mcA);
                }

                assignmentSetRepository.SaveAssignmentSet(model.AssignmentSet);
                TempData["message"] = $"Opgaven '{model.AssignmentSet.Title}' blev gemt.";
                return RedirectToAction(nameof(List));
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
            return RedirectToAction(nameof(List));
        }
    }
}