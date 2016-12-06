using System.Linq;
using System.Threading.Tasks;
using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADL.Controllers
{
    public class ClassController : Controller 
    {
        private readonly IClassRepository classRepository;
        private readonly UserManager<Person> userManager;
        public ClassController(IClassRepository classRepo, UserManager<Person> usrMgr)
        {
            classRepository = classRepo;
            userManager = usrMgr;
        }

        public Task<Person> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        public async Task<ViewResult> Create()
        {
            int currentSchoolId = (await GetCurrentUserAsync()).SchoolId;

            return View(new Class() { SchoolId = currentSchoolId});
        }

        [HttpPost]
        public IActionResult Create(Class newClass)
        {
            if(ModelState.IsValid)
            {
                classRepository.SaveClass(newClass);
                return View(nameof(List));
            }
            return View(newClass);
        }

        public async Task<ViewResult> List()
        {
            int currentSchoolId = (await GetCurrentUserAsync()).SchoolId;
            return View(classRepository.Classes.Where(c => c.SchoolId == currentSchoolId));
        }
    }
}