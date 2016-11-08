using Microsoft.AspNetCore.Mvc;
using System;

namespace ADL.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}