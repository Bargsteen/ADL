using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;

namespace ADL.Controllers
{
    public class LocationController : Controller
    {
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Create(Assignment assignment)
        {
            return View();
        }

        public ViewResult Delete(int? assignmentId)
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int? assignmentId)
        {
            return View();
        }
        [HttpPost]
        public ViewResult Edit(Assignment editedAssignment)
        {
            return View();
        }

        public ViewResult List()
        {
            return View();
        }
        public ViewResult Solve()
        {
            return View();
        }
    }
}