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
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Create(Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                repository.Add(assignment);
                return View("SuccesfullyCreated");
            }
            return View();
        }

        public ViewResult Delete(int? assignmentId)
        {
            Assignment assignment = repository.Assignments.FirstOrDefault(a => a.AssignmentID == assignmentId);
            if(assignment != null)
            {
                repository.Delete(assignment);
            }
            return View(nameof(List), repository.Assignments);
        }
        [HttpGet]
        public ViewResult Edit(int? assignmentId)
        {
            Assignment assignment = repository.Assignments.FirstOrDefault(a => a.AssignmentID == assignmentId);
            if(assignment != null)
            {
                return View(assignment);
            }
            return View(nameof(List), repository.Assignments);
        }
        [HttpPost]
        public ViewResult Edit(Assignment editedAssignment)
        {
            repository.Edit(editedAssignment);
            return View("SuccesfullyEdited");
        }

        public ViewResult List()
        {
            return View(repository.Assignments);
        }
        public ViewResult Solve()
        {
            return View();
        }
    }
}