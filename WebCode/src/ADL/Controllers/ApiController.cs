using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;

namespace ADL.Controllers
{
    public class ApiController : Controller
    {
        IAssignmentRepository repository;
        public ApiController(IAssignmentRepository repo)
        {
            repository = repo;
        }

        public JsonResult GetAssignment(int? id)
        {
            Assignment assignment = repository.Assignments.FirstOrDefault(a => a.AssignmentID == id);
            if(assignment != null)
            {
                return Json(assignment);
            }
            return null;
        }
       
    }
}