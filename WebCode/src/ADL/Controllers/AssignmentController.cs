using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using ADL.Models.ViewModels;

namespace ADL.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentRepository assignmentRepository;
        ILocationRepository locationRepository;
        public AssignmentController(IAssignmentRepository assignmentRepo, ILocationRepository locationRepo)
        {
            assignmentRepository = assignmentRepo;
            locationRepository = locationRepo;
        }

        public ViewResult List() => View(assignmentRepository.Assignments);

        public ViewResult Edit(int assignmentId)
        {
            return View(assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == assignmentId));
        }

        [HttpPost]
        public IActionResult Edit(Assignment assignment)
        {
            if(ModelState.IsValid)
            {
                assignmentRepository.SaveAssignment(assignment);
                TempData["message"] = $"{assignment.Headline} blev gemt.";
                return RedirectToAction(nameof(List));
            }
            // Something was wrong with the entered data
            return View(assignment);
        }

        // Uses the edit view, but gives it a new assignment.
        public ViewResult Create() => View(nameof(Edit), new Assignment());

        [HttpPost]
        public IActionResult Delete(int assignmentId)
        {
            Assignment deletedAssignment = assignmentRepository.DeleteAssignment(assignmentId);
            if(deletedAssignment != null)
            {
                TempData["message"] = $"{deletedAssignment.Headline} blev slettet.";
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
        public IActionResult AttachAssignmentToLocation(AssignmentToLocationAttachment attachment)
        {
            Location chosenLocation = locationRepository.Locations.FirstOrDefault(l => l.LocationId == attachment.ChosenLocationId);
            Assignment chosenAssignment = assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == attachment.ChosenAssignmentId);
            if(chosenLocation != null && chosenAssignment != null)
            {
                locationRepository.SaveAttachedAssignmentId(chosenLocation.LocationId, chosenAssignment.AssignmentId);
                TempData["message"] = $"Opgaven '{chosenAssignment.Headline}' blev koblet med lokationen '{chosenLocation.Title}'";
                return RedirectToAction(nameof(List));
            }
            return View(attachment);
        }


        
    
    }
}