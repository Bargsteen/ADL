using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using QRCoder;
using System.IO;
using System.Collections;
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
        
        public ViewResult Delete(int? locationId)
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

        public ViewResult Generate_QR(int locationID)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(locationID.ToString(), QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);

            ViewData["QRCode"] = ByteArraytoURL(qrCode.GetGraphic(20)); ;
            return View("ViewQR");
        }

        public string ByteArraytoURL(Byte[] qrCodeByteArray)
        {
            string imageBase64Data = Convert.ToBase64String(qrCodeByteArray);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);

            return imageDataURL;
        }

        public ViewResult TEST()
        {
            return View();
        }

    }
}