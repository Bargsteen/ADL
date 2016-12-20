using System.Linq;
using System.Threading.Tasks;
using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer,Admin")]
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly UserManager<Person> _userManager;
        public ClassController(IClassRepository classRepo, UserManager<Person> usrMgr)
        {
            _classRepository = classRepo;
            _userManager = usrMgr;
        }

        public Task<Person> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<ViewResult> Create()
        {
            int currentSchoolId = (await GetCurrentUserAsync()).SchoolId;

            return View(new Class() { SchoolId = currentSchoolId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Class newClass)
        {
            if (ModelState.IsValid)
            {
                _classRepository.SaveClass(newClass);
                TempData["message"] = $"Klassen '{newClass.StartYear} {newClass.Name}' blev oprettet.";
                int currentUserSchoolId = (await GetCurrentUserAsync()).SchoolId;
                return View("List", _classRepository.Classes.Where(c => c.SchoolId == currentUserSchoolId));
            }
            return View(newClass);
        }
        public async Task<ViewResult> List()
        {
            int currentSchoolId = (await GetCurrentUserAsync()).SchoolId;
            return View(_classRepository.Classes.Where(c => c.SchoolId == currentSchoolId));
        }
    }
}