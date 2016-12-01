using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ADL.Controllers
{
    public class ApiController : Controller
    {
        private IAssignmentSetRepository assignmentSetRepository;
        private ILocationRepository locationRepository;
        private IAnswerRepository answerRepository;
        private UserManager<Person> userManager;
        private SignInManager<Person> signInManager;
        public ApiController(IAssignmentSetRepository assignmentRepo, ILocationRepository locationRepo, IAnswerRepository answerRepo, UserManager<Person> userMgr,
                SignInManager<Person> signinMgr)
        {
            assignmentSetRepository = assignmentRepo;
            locationRepository = locationRepo;
            answerRepository = answerRepo;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        public async Task<string> Location(int? id, string userId)
        {
            Location location = locationRepository.Locations.FirstOrDefault(l => l.LocationId == id);
            if (location != null)
            {
                Assignment assignment = assignmentSetRepository.AssignmentSets
                    .FirstOrDefault(aS => aS.AssignmentSetId == location.AttachedAssignmentSetId)
                    .Assignments.FirstOrDefault(a => a.AssignmentId == location.AttachedAssignmentId);

                if (assignment != null)
                {
                    if (await IsValidUser(userId))
                    {
                        return JsonConvert.SerializeObject(assignment);
                    }
                    else
                    {
                        return "Brugeren blev ikke genkendt.";
                    }

                }
                return "Lokationen har ikke nogen opgave";
            }
            return "Lokationen eksisterer ikke";
        }

        public async Task<string> LocationList(string userId)
        {
            if (await IsValidUser(userId))
            {
                List<Location> allLocationsWithAssignments = locationRepository.Locations.Where(l => l.AttachedAssignmentId != 0).ToList();
                return JsonConvert.SerializeObject(allLocationsWithAssignments);
            }
            else
            {
                return "Brugeren blev ikke genkendt.";
            }

        }

        [HttpPost]
        public async Task<string> SendAnswer([FromBody]Answer answer)
        {
            string reply;
            if (answer != null)
            {
                if (await IsValidUser(answer.UserId))
                {
                    Assignment answeredAssignment = assignmentSetRepository.AssignmentSets
                    .FirstOrDefault(a => a.AssignmentSetId == answer.AnsweredAssignmentSetId)
                    .Assignments.FirstOrDefault(a => a.AssignmentId == answer.AnsweredAssignmentId);
                    
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
                else
                {
                    reply = "Brugeren blev ikke genkendt.";
                }
            }
            else
            {
                reply = "Svaret havde ikke korrekt format.";
            }
            return reply;
        }

        [HttpPost]
        public async Task<string> GetIdentity([FromBody]LoginModel model)
        {
            Person user = await userManager.FindByNameAsync(model.Username);
            IdentificationResult identificationResult = new IdentificationResult();
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    identificationResult.IsAuthenticated = true;
                    identificationResult.UserId = user.Id;
                    return JsonConvert.SerializeObject(identificationResult);
                }

            }

            // Credentials are invalid, or account doesn't exist
            identificationResult.IsAuthenticated = false;
            return JsonConvert.SerializeObject(identificationResult);
        }

        public async Task<bool> IsValidUser(string userId)
        {
            return await userManager.FindByIdAsync(userId) != null;
        }

    }
}