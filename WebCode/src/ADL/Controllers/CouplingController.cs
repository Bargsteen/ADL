using System.Collections.Generic;
using ADL.Models;
using ADL.Models.Repositories;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ADL.Controllers
{
    public class CouplingController : Controller
    {
        private readonly ILocationRepository locationRepository;
        public CouplingController(ILocationRepository locationRepo)
        {
            locationRepository = locationRepo;
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

        public ViewResult AttachAssignmentToLocation(int chosenAssignmentId)
        {
            AssignmentToLocationAttachment attachment = new AssignmentToLocationAttachment();
            attachment.ChosenAssignmentId = chosenAssignmentId;
            attachment.Locations = locationRepository.Locations;
            return View(attachment);
        }

                /*[HttpPost]
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
*/

    }
}