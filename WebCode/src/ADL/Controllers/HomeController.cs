using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System;

namespace ADL.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View();
        public ViewResult Download() => View();
    }
}