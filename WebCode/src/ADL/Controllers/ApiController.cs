using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ADL.Controllers
{
    public class ApiController : Controller
    {
        private IAssignmentRepository assignmentRepository;
        private ILocationRepository locationRepository;
        private Random random = new Random();
        public ApiController(IAssignmentRepository assignmentRepo, ILocationRepository locationRepo)
        {
            assignmentRepository = assignmentRepo;
            locationRepository = locationRepo;
        }

        public string GetAssignment(int? id)
        {
            Assignment assignment = assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == id);
            if(assignment != null)
            {
                return JsonConvert.SerializeObject(assignment);
            }
            return "Invalid AssignmentID given.";
        }

        public string Location(int? id)
        {
            Location location = locationRepository.Locations.FirstOrDefault(l => l.LocationId == id);
            if(location != null)
            {
                Assignment randomAssignment = assignmentRepository.Assignments.ElementAt(random.Next(0, assignmentRepository.Assignments.Count()));
                if(randomAssignment != null)
                {
                    return JsonConvert.SerializeObject(randomAssignment);
                }
            }
            return "Location does not exist";
        }

        public string LocationList()
        {
            List<int> allLocationIds = locationRepository.Locations.Select(l => l.LocationId).ToList();
            return JsonConvert.SerializeObject(allLocationIds);
        }
    }
}