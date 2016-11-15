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

        public ViewResult Edit(int locationId) =>
            View(repository.Locations.FirstOrDefault(l => l.LocationID == locationId));

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid) {
                repository.SaveLocation(location);
                //TempData["message"] = $"{location.Title} has been saved";
                return RedirectToAction("Index");
            } else {
                // there is something wrong with the data values
                return View(location);
            }
        }

        public ViewResult Create() => View(nameof(Edit), new Location());

        [HttpPost]
        public IActionResult Delete(int locationId) {
            Location deletedLocation = repository.DeleteLocation(locationId);
           /* if (deletedLocation != null) {
                TempData["message"] = $"{deletedLocation.Title} was deleted";
            }*/
            return RedirectToAction("Index");
        }

        public ViewResult List()
        {
            return View(repository.Locations);
        }
    }
}