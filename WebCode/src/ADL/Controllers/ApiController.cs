using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using Newtonsoft.Json;

namespace ADL.Controllers
{
    public class ApiController : Controller
    {
        private IAssignmentRepository repository;
        public ApiController(IAssignmentRepository repo)
        {
            repository = repo;
        }

        public string GetAssignment(int? id)
        {
            Assignment assignment = repository.Assignments.FirstOrDefault(a => a.AssignmentID == id);
            if(assignment != null)
            {
                return JsonConvert.SerializeObject(assignment);
            }
            return "invalid";
        }
       
    }
}