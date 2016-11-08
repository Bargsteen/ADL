using Microsoft.AspNetCore.Mvc;
using System;

namespace ADL.Controllers
{
    public class AboutController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
        
    }
}