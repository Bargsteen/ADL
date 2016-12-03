using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using System.Collections.Generic;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using ADL.Infrastructure;
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



        public ViewResult StudentPick()
        {
            PersonAndAssignmentViewModel studentList = new PersonAndAssignmentViewModel()
            {
                /*TEST ANDREAS*/
                AssignmentSets = new List<AssignmentSet>() { new AssignmentSet(), new AssignmentSet() },

                Persons = new List<Person>() { new Person(), new Person(), new Person(), new Person(), new Person() }
                
                //AssignmentSets = assignmentSetRepository.AssignmentSets,
                //Persons = userManager.Users as IEnumerable<Person>
            };
            return View(studentList);
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

        // Uses the edit view, but gives it a new assignment. j
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

        public ViewResult AttachAssignmentToLocation(int chosenAssignmentId)
        {
            AssignmentToLocationAttachment attachment = new AssignmentToLocationAttachment();
            attachment.ChosenAssignmentId = chosenAssignmentId;
            attachment.Locations = locationRepository.Locations;
            return View(attachment);
        }

        [HttpPost]
        public async Task<IActionResult> AttachAssignmentToLocation(AssignmentToLocationAttachment attachment)
        {
            Location chosenLocation = locationRepository.Locations.FirstOrDefault(l => l.LocationId == attachment.ChosenLocationId);
            Assignment chosenAssignment = assignmentSetRepository.AssignmentSets.FirstOrDefault(b => b.AssignmentSetId == attachment.ChosenAssignmentSetId).Assignments.FirstOrDefault(a => a.AssignmentId == attachment.ChosenAssignmentId);
            Person chosenPerson = await GetCurrentUserAsync();
            if (chosenLocation != null && chosenAssignment != null)
            {
                locationRepository.SaveAttachedAssignmentId(chosenLocation.LocationId, chosenPerson.Id, chosenAssignment.AssignmentId);
                TempData["message"] = $"Opgaven '{chosenAssignment.Title}' blev koblet med lokationen '{chosenLocation.Title}'";
                return RedirectToAction(nameof(List));
            }
            return View(attachment);
        }




    }
}