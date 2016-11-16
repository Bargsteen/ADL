using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using QRCoder;
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

        public ViewResult Edit(int locationId) =>
            View(repository.Locations.FirstOrDefault(l => l.LocationId == locationId));

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid) {
                repository.SaveLocation(location);
                //TempData["message"] = $"{location.Title} has been saved";
                return RedirectToAction("SuccesfullyCreated", location);
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

        public ViewResult Generate_QR(int locationID)

        {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(locationID.ToString(), QRCodeGenerator.ECCLevel.Q);

            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData); ViewData["QRCode"] = ByteArraytoURL(qrCode.GetGraphic(20)); ;

            return View("ViewQR");

        }

        public string ByteArraytoURL(Byte[] qrCodeByteArray)

        {

            string imageBase64Data = Convert.ToBase64String(qrCodeByteArray);

            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data); return imageDataURL;

        }

        public ViewResult List()
        {
            return View(repository.Locations);
        }
    }
}