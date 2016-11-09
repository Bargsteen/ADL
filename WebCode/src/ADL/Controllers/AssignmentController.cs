using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System;

namespace ADL.Controllers
{
    public class SolvingAssignmentController : Controller
    {
        IAssignmentRepository assignments;
        public SolvingAssignmentController (IAssignmentRepository repo)
        {
            assignments = repo;
        }
        public ViewResult Solve()
        {
            return View();
        }
    }
}