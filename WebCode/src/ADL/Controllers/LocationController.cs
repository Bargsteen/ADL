using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer")]
    public class LocationController : Controller
    {
        ILocationRepository locationRepository;

        public LocationController(ILocationRepository repo)
        {
            locationRepository = repo;
        }

        public ViewResult List() => View(locationRepository.Locations);

        public ViewResult Edit(int locationId) =>
            View(locationRepository.Locations
                .FirstOrDefault(l => l.LocationId == locationId));

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid) {
                locationRepository.SaveLocation(location);
                TempData["message"] = $"Lokationen '{location.Title}' blev gemt.";
                return RedirectToAction(nameof(List));
            } else {
                // there is something wrong with the data values
                return View(location);
            }
        }

        // Uses edit view, but gives it a new location as input
        public ViewResult Create() => View(nameof(Edit), new Location());

        [HttpPost]
        public IActionResult Delete(int locationId) {
            Location deletedLocation = locationRepository.DeleteLocation(locationId);
            if (deletedLocation != null) {
                TempData["message"] = $"Lokationen '{deletedLocation.Title}' blev slettet.";
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult CreateQR(int locationId, string title)
        {
            QrGenerator.GenerateQR(locationId);
            return View("ViewQR", title);
        }
    }
}