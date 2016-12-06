using System.Collections.Generic;
using System.Linq;
using ADL.Models;
using ADL.Models.Repositories;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ADL.Controllers
{
    public class CouplingController : Controller
    {
        private readonly ILocationRepository locationRepository;
        private readonly IClassRepository classRepository;
        private readonly IAssignmentSetRepository assignmentSetRepository;
        public CouplingController(ILocationRepository locationRepo, IClassRepository classRepo, IAssignmentSetRepository assignmentSetRepo)
        {
            locationRepository = locationRepo;
            classRepository = classRepo;
            assignmentSetRepository = assignmentSetRepo;
        }

        public ViewResult ChooseClass(int currentSchoolId, int chosenAssignmentSetId)
        {

            ChooseClassViewModel model = new ChooseClassViewModel()
            {
                ChosenAssignmentSetId = chosenAssignmentSetId,
                AvailableClasses = classRepository.Classes.Where(c => c.SchoolId == currentSchoolId)
            };
            return View(model);
        }

        public ViewResult Differentiate(int chosenAssignmentSetId, int chosenClassId)
        {
            var chosenClass = classRepository.Classes.FirstOrDefault(c => c.ClassId == chosenClassId);
            var chosenAssignmentSet = assignmentSetRepository.AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == chosenAssignmentSetId);
            if (chosenClass != null && chosenAssignmentSet != null)
            {
                DifferentiateViewModel model = new DifferentiateViewModel()
                {
                    ChosenClass = chosenClass,
                    ChosenAssignmentSet = chosenAssignmentSet
                };

                return View(model);
            }
            TempData["errorMessage"] = "Den valgte kombination var invalid."; // Should never happen
            return View();
        }

        [HttpPost]
        public IActionResult Differentiate(DifferentiateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ChooseLocationsViewModel locationModel = new ChooseLocationsViewModel()
                {
                    PersonAssignmentCouplings = new List<PersonAssignmentCoupling>(),
                    ChosenLocations = new List<ChosenLocation>(),
                    AvailableLocations = locationRepository.Locations.Where(l => l.SchoolId == model.CurrentSchoolId).ToList()

                };
                foreach (var possibleCoupling in model.PersonAssignmentCouplings)
                {
                    if (possibleCoupling.IsChosen == true)
                    {
                        var newCoupling = new PersonAssignmentCoupling()
                        {
                            PersonId = possibleCoupling.PersonId,
                            AssignmentId = possibleCoupling.AssignmentId
                        };
                        locationModel.PersonAssignmentCouplings.Add(newCoupling);
                    }
                }
                return View(nameof(ChooseLocations), locationModel);

            }
            return View();
        }


        public ViewResult ChooseLocations(ChooseLocationsViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult FinishCoupling(ChooseLocationsViewModel model)
        {
            if (ModelState.IsValid)
            {
              

                TempData["errorMessage"] = "Koblingen blev teknisk set færdig, men blev ikke gemt.";
                return View();
            }
            TempData["errorMessage"] = "Der skete en fejl.";
            return View(nameof(ChooseLocations), model);
        }



        public ViewResult StudentPick()
        {
            PersonAndAssignmentViewModel studentList = new PersonAndAssignmentViewModel()
            {
                /*TEST ANDREAS*/
                AssignmentSets = new List<AssignmentSet>() { new AssignmentSet(), new AssignmentSet() },

                Persons = new List<Person>() { new Person(), new Person(), new Person(), new Person(), new Person() }

                //AssignmentSets = assignmentSetRepository.AssignmentSets,
                //Persons = userManager.Users as IEnumerable<Person>
            };
            return View(studentList);
        }

        public ViewResult AttachAssignmentToLocation(int chosenAssignmentId)
        {
            AssignmentToLocationAttachment attachment = new AssignmentToLocationAttachment();
            attachment.ChosenAssignmentId = chosenAssignmentId;
            attachment.Locations = locationRepository.Locations;
            return View(attachment);
        }

        /*[HttpPost]
public async Task<IActionResult> AttachAssignmentToLocation(AssignmentToLocationAttachment attachment)
{
    Location chosenLocation = locationRepository.Locations.FirstOrDefault(l => l.LocationId == attachment.ChosenLocationId);
    Assignment chosenAssignment = assignmentSetRepository.AssignmentSets.FirstOrDefault(b => b.AssignmentSetId == attachment.ChosenAssignmentSetId).Assignments.FirstOrDefault(a => a.AssignmentId == attachment.ChosenAssignmentId);
    Person chosenPerson = await GetCurrentUserAsync();
    if (chosenLocation != null && chosenAssignment != null)
    {
        locationRepository.SaveAttachedAssignmentId(chosenLocation.LocationId, chosenPerson.Id, chosenAssignment.AssignmentId);
        TempData["message"] = $"Opgaven '{chosenAssignment.Title}' blev koblet med lokationen '{chosenLocation.Title}'";
        return RedirectToAction(nameof(List));
    }
    return View(attachment);
}
*/

    }
}