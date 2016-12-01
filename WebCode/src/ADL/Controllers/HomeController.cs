using Microsoft.AspNetCore.Mvc;

namespace ADL.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View();
        public ViewResult Download() => View();
    }
}