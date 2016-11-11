using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;

namespace ADL.Controllers
{
    public class LocationController : Controller
    {
        ILocationRepository repository;
        public LocationController(ILocationRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                repository.Save(location);
                return View("SuccesfullyCreated");
            }
            return View();
        }

        public ViewResult Delete(int? assignmentId)
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