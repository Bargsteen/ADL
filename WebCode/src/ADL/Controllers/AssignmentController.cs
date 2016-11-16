using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;

namespace ADL.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentRepository repository;
        public AssignmentController(IAssignmentRepository repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Assignments);

        public ViewResult Edit(int assignmentId)
        {
            return View(repository.Assignments.FirstOrDefault(a => a.AssignmentId == assignmentId));
        }

        [HttpPost]
        public IActionResult Edit(Assignment assignment)
        {
            if(ModelState.IsValid)
            {
                repository.SaveAssignment(assignment);
                //TempData["message"] = $"{assignment.Headline} blev gemt.";
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
            Assignment deletedAssignment = repository.DeleteAssignment(assignmentId);
            if(deletedAssignment != null)
            {
                //TempData["message"] = $"{deletedAssignment.Headline} blev slettet";
            }
            return RedirectToAction(nameof(List));
        }

        


        
    
    }
}