using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System;

namespace ADL.Controllers
{
    public class HomeController : Controller
    {
        IAssignmentRepository assignments;
        public HomeController (IAssignmentRepository repo)
        {
            assignments = repo;
        }
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Time()
        {
            return View(DateTime.Now);
        }
    }
}