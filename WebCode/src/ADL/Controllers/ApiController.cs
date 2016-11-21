using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADL.Controllers
{
    public class ApiController : Controller
    {
        private IAssignmentRepository assignmentRepository;
        private ILocationRepository locationRepository;
        private IAnswerRepository answerRepository;
        public ApiController(IAssignmentRepository assignmentRepo, ILocationRepository locationRepo, IAnswerRepository answerRepo)
        {
            assignmentRepository = assignmentRepo;
            locationRepository = locationRepo;
            answerRepository = answerRepo;
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
            List<int> allLocationIds = locationRepository.Locations.Select(l => l.LocationId).ToList();
            return JsonConvert.SerializeObject(allLocationIds);
        }
        /*public ViewResult ReceiveAnswer(int id)
        {
            Location location = locationRepository.Locations.FirstOrDefault(l => l.LocationId == id);
            Assignment assignment = assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == location.AttachedAssignmentId);
            if (location != null && assignment != null)
            {
                Answer answer = new Answer() { AnsweredAssignment = assignment };
                return View(answer);
            }
            return View(id);
        }
*/

        [HttpPost]
        public void ReceiveAnswer([FromBody]string a)
        {
            
            if (a != null)
            {
                Assignment assignment = new Assignment() { Headline = a, Question = "qq", CorrectAnswer = 0 };
                assignmentRepository.SaveAssignment(assignment);
            }
        }

    }
}