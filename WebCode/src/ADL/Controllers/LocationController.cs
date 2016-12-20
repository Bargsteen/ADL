using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using ADL.Models.Repositories;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer,Admin")]
    public class LocationController : Controller
    {
        readonly ILocationRepository _locationRepository;
        readonly UserManager<Person> _userManager;
        public LocationController(ILocationRepository repo, UserManager<Person> usrMgr)
        {
            _locationRepository = repo;
            _userManager = usrMgr;
        }

        public async Task<ViewResult> List()
        {
            Person currentUser = await GetCurrentUserAsync();
            return View(_locationRepository.Locations.Where(l => l.SchoolId == currentUser?.SchoolId));
        }


        public ViewResult Edit(int locationId) =>
            View(_locationRepository.Locations
                .FirstOrDefault(l => l.LocationId == locationId));

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _locationRepository.SaveLocation(location);
                TempData["message"] = $"Lokationen '{location.Title}' blev gemt.";
                return RedirectToAction(nameof(List));
            }
            else
            {
                // there is something wrong with the data values
                return View(location);
            }
        }

        // Uses edit view, but gives it a new location as input
        public async Task<ViewResult> Create()
        {
            Person currentUser = await GetCurrentUserAsync();
            Location newLocation = new Location()
            {
                SchoolId = currentUser.SchoolId
            };
            return View(nameof(Edit), newLocation);
        }

        [HttpPost]
        public IActionResult Delete(int locationId)
        {
            Location deletedLocation = _locationRepository.DeleteLocation(locationId);
            if (deletedLocation != null)
            {
                TempData["message"] = $"Lokationen '{deletedLocation.Title}' blev slettet.";
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult CreateQr(int locationId, string title)
        {
            QrGenerator.GenerateQr(locationId);
            return View("ViewQR", title);
        }
        public Task<Person> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}