using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ADL.Controllers
{

    [Authorize(Roles = "Lærer")]
    public class SchoolController : Controller
    {
        ISchoolRepository schoolRepository;
        public SchoolController(ISchoolRepository repo)
        {
            schoolRepository = repo;
        }

        public ViewResult List() => View(schoolRepository.Schools);

        public ViewResult Edit(int SchoolId) =>
            View(schoolRepository.Schools
                .FirstOrDefault(l => l.SchoolId == SchoolId));

        [HttpPost]
        public IActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                schoolRepository.SaveSchool(school);
                TempData["message"] = $"Lokationen '{school.SchoolName}' blev gemt.";
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
            School deletedSchool = schoolRepository.DeleteSchool(schoolId);
            if (deletedSchool != null)
            {
                TempData["message"] = $"Skole '{deletedSchool.SchoolName}' blev slettet.";
            }
            return RedirectToAction(nameof(List));
        }
    }
}

