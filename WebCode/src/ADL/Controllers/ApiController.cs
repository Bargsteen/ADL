using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using ADL.Models.Repositories;
using ADL.Models.Assignments;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ADL.Models.Answers;
using Microsoft.AspNetCore.Identity;

namespace ADL.Controllers
{
    public class ApiController : Controller
    {
        private readonly IAssignmentSetRepository _assignmentSetRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        public ApiController(IAssignmentSetRepository assignmentRepo, ILocationRepository locationRepo, IAnswerRepository answerRepo, UserManager<Person> userMgr,
                SignInManager<Person> signinMgr)
        {
            _assignmentSetRepository = assignmentRepo;
            _locationRepository = locationRepo;
            _answerRepository = answerRepo;
            _userManager = userMgr;
            _signInManager = signinMgr;
        }
        /*tager en location og personId som input, skider en assignment ud der er serializaed*/
        public string Location(int? id, string personId)
        {
            Location location = _locationRepository.Locations.FirstOrDefault(l => l.LocationId == id);

            if (location != null) // this is a valid location
            {

                var firstCouplingWithThisPerson = location.PersonAssignmentCouplings.FirstOrDefault(pac => pac.PersonId == personId);
                if(firstCouplingWithThisPerson != null) // there is a coupling with this person
                {
                    var allAssignments = _assignmentSetRepository.AssignmentSets.SelectMany(a => a.Assignments);
                    Assignment assignmentToGiveToUser = allAssignments.FirstOrDefault(a => a.AssignmentId == firstCouplingWithThisPerson.AssignmentId);
                    if(assignmentToGiveToUser != null) // assignment was found
                    {
                        _locationRepository.RemoveSpecificCouplingOnLocation(location.LocationId, firstCouplingWithThisPerson);
                        return JsonConvert.SerializeObject(assignmentToGiveToUser);
                    }
                    return "Opgaven blev ikke fundet";
                }
                return "Lokationen har ikke nogen opgave";
            }
            return "Lokationen eksisterer ikke";
        }
        /*skal have alle de locations som en person er connected til*/
        public async Task<string> LocationList(string personId)
        {
            if (await IsValidUser(personId))
            {
                List<Location> allLocationsWithAssignments = _locationRepository.Locations.Where(l => l.PersonAssignmentCouplings.Exists(pa => pa.PersonId == personId)).ToList();
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
                    Assignment answeredAssignment =
                        _assignmentSetRepository.AssignmentSets.SelectMany(set => set.Assignments)
                            .First(a => a.AssignmentId == answer.AnsweredAssignmentId);

                    if (answeredAssignment != null)
                    {
                        _answerRepository.SaveAnswer(answer);
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
            Person user = await _userManager.FindByNameAsync(model.Username);
            IdentificationResult identificationResult = new IdentificationResult();
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
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
            return await _userManager.FindByIdAsync(userId) != null;
        }

    }
}