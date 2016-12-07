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
                    ChosenAssignmentSet = chosenAssignmentSet,
                    CurrentSchoolId = chosenClass.SchoolId
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
                Dictionary<int, List<PersonAssignmentCoupling>> chosenLocations = model.ChosenLocations.Where(cl => cl.IsChosen == true).ToDictionary(key => key.LocationId, val => new List<PersonAssignmentCoupling>());
                int amountOfLocations = chosenLocations.Count();
                List<string> chosenPeopleIds = model.PersonAssignmentCouplings.Select(pac => pac.PersonId).Distinct().ToList();
                foreach (var personId in chosenPeopleIds)
                {
                    foreach (var locationId in chosenLocations.Keys)
                    {
                        // Clear old couplings for this person on the chosen locations
                        locationRepository.RemoveAllCouplingsForSpecificPersonOnLocation(locationId, personId);
                    }
                    List<int> assignmentsForPerson = model.PersonAssignmentCouplings.Where(pac => pac.PersonId == personId).Select(pac => pac.AssignmentId).ToList();
                    List<int> locationsForPerson = chosenLocations.Keys.ToList();
                    int differenceBetweenAssignmentsAndLocationsCount = assignmentsForPerson.Count() - locationsForPerson.Count();


                    if (differenceBetweenAssignmentsAndLocationsCount > 0) // there are more assignments than locations
                    {
                        int indexOfChosenLocations = 0;
                        while (differenceBetweenAssignmentsAndLocationsCount > 0)
                        { // adds locations in order from chosenLocationsIds until enough locationsForPerson exist.
                            locationsForPerson.Add(chosenLocations.Keys.ElementAt(indexOfChosenLocations % amountOfLocations));
                            indexOfChosenLocations++;
                            differenceBetweenAssignmentsAndLocationsCount--;
                        }
                    }
                    foreach(int assignmentId in assignmentsForPerson)
                    {
                        // Choose a location, where a coupling should be made
                        // locationsForPersons contains at least as many locations as there are assignments.
                        var someLocationId = locationsForPerson.First();
                        locationsForPerson.Remove(someLocationId);
                        var newCoupling = new PersonAssignmentCoupling()
                        {
                            AssignmentId = assignmentId,
                            PersonId = personId
                            
                        };
                        chosenLocations[someLocationId].Add(newCoupling);
                    }

                }

                foreach(var kvp in chosenLocations)
                { // Save the changes to db
                    locationRepository.AddCouplingsToLocation(kvp.Key, kvp.Value);
                }

                TempData["message"] = "Koblingen blev gemt.";
                return View();
            }
            TempData["errorMessage"] = "Der skete en fejl.";
            return View(nameof(ChooseLocations), model);
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