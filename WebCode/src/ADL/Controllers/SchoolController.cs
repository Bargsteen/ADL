using System.Linq;
using ADL.Models;
using ADL.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ADL.Controllers
{

    [Authorize(Roles = "Admin")]
    public class SchoolController : Controller
    {
        readonly ISchoolRepository _schoolRepository;
        public SchoolController(ISchoolRepository repo)
        {
            _schoolRepository = repo;
        }

        public ViewResult List() => View(_schoolRepository.Schools);

        public ViewResult Edit(int schoolId) =>
            View(_schoolRepository.Schools
                .FirstOrDefault(l => l.SchoolId == schoolId));

        [HttpPost]
        public IActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                _schoolRepository.SaveSchool(school);
                TempData["message"] = $"Skolen ved navn '{school.SchoolName}' blev gemt.";
                return RedirectToAction(nameof(List));
            }
            else
            {
                // there is something wrong with the data values
                return View(school);
            }
        }

        // Uses edit view, but gives it a new school as input
        public ViewResult Create() => View(nameof(Edit), new School());

        [HttpPost]
        public IActionResult Delete(int schoolId)
        {
            School deletedSchool = _schoolRepository.DeleteSchool(schoolId);
            if (deletedSchool != null)
            {
                TempData["message"] = $"Skole '{deletedSchool.SchoolName}' blev slettet.";
            }
            return RedirectToAction(nameof(List));
        }

    }
}

