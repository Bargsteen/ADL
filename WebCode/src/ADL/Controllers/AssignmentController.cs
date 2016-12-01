using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using ADL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ADL.Controllers
{
    [Authorize(Roles = "Lærer")]
    public class AssignmentController : Controller
    {
        IAssignmentSetRepository assignmentSetRepository;
        ILocationRepository locationRepository;

        UserManager<Person> userManager;

        public AssignmentController(IAssignmentSetRepository assignmentSetRepo, ILocationRepository locationRepo, UserManager<Person> usrMgr)
        {
            assignmentSetRepository = assignmentSetRepo;
            locationRepository = locationRepo;
            userManager = usrMgr;
        }

        public ViewResult List()
        {
            AssignmentAndLocationListViewModel assignmentList = new AssignmentAndLocationListViewModel()
            {
                AssignmentSets = assignmentSetRepository.AssignmentSets,
                Locations = locationRepository.Locations
            };
            return View(assignmentList);
        }

        public ViewResult Edit(int assignmentSetId)
        {
            return View(assignmentSetRepository.AssignmentSets.FirstOrDefault(a => a.AssignmentSetId == assignmentSetId));
        }

        [HttpPost]
        public IActionResult Edit(AssignmentSet assignmentSet)
        {
            if(ModelState.IsValid)
            {
                assignmentSetRepository.SaveAssignmentSet(assignmentSet);
                TempData["message"] = $"Opgaven '{assignmentSet.Title}' blev gemt.";
                return RedirectToAction(nameof(List));
            }
            // Something was wrong with the entered data
            return View(assignmentSet);
        }

        // Uses the edit view, but gives it a new assignment. j
        public async Task<ViewResult> Create()
        {
            Person currentUser = await GetCurrentUserAsync();
            AssignmentSet assignmentSet = new AssignmentSet() { CreatorId = currentUser.Id };
            return View(nameof(Edit), assignmentSet);
        }


        private Task<Person> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        public IActionResult DeleteAssignmentSet(int assignmentSetId)
        {
            AssignmentSet deletedAssignmentSet = assignmentSetRepository.DeleteAssignmentSet(assignmentSetId);
            if(deletedAssignmentSet != null)
            {
                TempData["message"] = $"Opgaven '{deletedAssignmentSet.Title}' blev slettet.";
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult AttachAssignmentToLocation(int chosenAssignmentId)
        {
            AssignmentToLocationAttachment attachment = new AssignmentToLocationAttachment();
            attachment.ChosenAssignmentId = chosenAssignmentId;
            attachment.Locations = locationRepository.Locations;
            return View(attachment);
        }
        [HttpPost]
        public IActionResult AttachAssignmentToLocation(AssignmentToLocationAttachment attachment)
        {
            Location chosenLocation = locationRepository.Locations.FirstOrDefault(l => l.LocationId == attachment.ChosenLocationId);
            Assignment chosenAssignment = assignmentSetRepository.AssignmentSets.FirstOrDefault(b => b.AssignmentSetId == attachment.ChosenAssignmentSetId)
                .Assignments.FirstOrDefault(a => a.AssignmentId == attachment.ChosenAssignmentId);
                
            if(chosenLocation != null && chosenAssignment != null)
            {
                locationRepository.SaveAttachedAssignmentId(chosenLocation.LocationId, chosenAssignment.AssignmentId);
                TempData["message"] = $"Opgaven '{chosenAssignment.Title}' blev koblet med lokationen '{chosenLocation.Title}'";
                return RedirectToAction(nameof(List));
            }
            return View(attachment);
        }


        
    
    }
}