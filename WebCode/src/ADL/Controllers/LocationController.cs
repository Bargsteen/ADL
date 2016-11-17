using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using System;

namespace ADL.Controllers
{
    public class LocationController : Controller
    {
        ILocationRepository repository;
        public LocationController(ILocationRepository repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Locations);

        public ViewResult Edit(int locationId) =>
            View(repository.Locations
                .FirstOrDefault(l => l.LocationId == locationId));

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid) {
                repository.SaveLocation(location);
                TempData["message"] = $"{location.Title} blev gemt.";
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
            Location deletedLocation = repository.DeleteLocation(locationId);
            if (deletedLocation != null) {
                TempData["message"] = $"{deletedLocation.Title} blev slettet.";
            }
            return RedirectToAction(nameof(List));
        }


        private string LocationIdtoPath(int locationID)
        {
            /*Combine the locationID and the path into a single string*/
            string path = $"~/images/{locationID}";

            return path;
        }

        public string ByteArraytoURL(Byte[] qrCodeByteArray)
        {
            return "";
        }


        private string QRByteArraytoImageDataURL(Byte[] inputByteArray)
        {
            /*Convert the input array into into a string with the base of 64*/
            string imageBase64Data = Convert.ToBase64String(inputByteArray);

            /*creates a readable URL to the previou string through a url*/
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

            return imageDataURL;
        }

        
    }
}