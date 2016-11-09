using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System;

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