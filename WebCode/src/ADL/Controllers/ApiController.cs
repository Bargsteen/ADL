using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ADL.Controllers
{
    [Authorize]
    public class ApiController : Controller
    { 
        private IAssignmentRepository assignmentRepository;
        private ILocationRepository locationRepository;
        private IAnswerRepository answerRepository;
        private UserManager<Person> userManager;
        private SignInManager<Person> signInManager;
        public ApiController(IAssignmentRepository assignmentRepo, ILocationRepository locationRepo, IAnswerRepository answerRepo, UserManager<Person> userMgr,
                SignInManager<Person> signinMgr)
        {
            assignmentRepository = assignmentRepo;
            locationRepository = locationRepo;
            answerRepository = answerRepo;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        public string GetAssignment(int? id)
        {
            Assignment assignment = assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == id);
            if (assignment != null)
            {
                return JsonConvert.SerializeObject(assignment);
            }
            return "Invalid AssignmentId given.";
        }

        public string Location(int? id)
        {
            Location location = locationRepository.Locations.FirstOrDefault(l => l.LocationId == id);
            if (location != null)
            {
                Assignment assignment = assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == location.AttachedAssignmentId);
                if (assignment != null)
                {
                    return JsonConvert.SerializeObject(assignment);
                }
                return "Lokationen har ikke nogen opgave";
            }
            return "Lokationen eksisterer ikke";
        }

        public string LocationList()
        {
            List<Location> allLocationsWithAssignments = locationRepository.Locations.Where(l => l.AttachedAssignmentId != 0).ToList();
            return JsonConvert.SerializeObject(allLocationsWithAssignments);
        }

        [HttpPost]
        public string SendAnswer([FromBody]Answer answer)
        {
            string reply = "Svaret havde ikke korrekt format.";
            if (answer != null)
            {
                Assignment answeredAssignment = assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == answer.AnsweredAssignmentId);
                if (answeredAssignment != null)
                {
                    answerRepository.SaveAnswer(answer);
                    reply = JsonConvert.SerializeObject(answer);
                }
                else
                {
                    reply = "Opgaven blev ikke fundet.";
                }
            }
            return reply;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Task<ClaimsIdentity>> GetIdentity([FromBody]LoginModel model)
        {
            Person user = await userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if(result.Succeeded)
                {
                    return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(model.Username, "ADL"), new Claim[] { }));
                }
                
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
        
    }
}