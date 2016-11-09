using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System;

namespace ADL.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentRepository assignments;
        public AssignmentController (IAssignmentRepository repo)
        {
            assignments = repo;
        }
        public ViewResult Create()
        {
            return View();
        }
        public ViewResult Solve()
        {
            return View();
        }
    }
}