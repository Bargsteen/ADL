using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ADL.Controllers
{
    [Authorize(Roles = "Elev")]
    public class SolvingController : Controller
    {
        /*private IAnswerRepository answerRepository;
        private IAssignmentRepository assignmentRepository;
        public SolvingController(IAnswerRepository answerRepo, IAssignmentRepository assignmentRepo)
        {
            answerRepository = answerRepo;
            assignmentRepository = assignmentRepo;
        }*/
        /*public ViewResult Solve(int? Id)
        {
            Assignment assignment = assignmentRepository.Assignments.FirstOrDefault(a => a.AssignmentId == Id);
            if (assignment != null)
            {
                Answer answer = new Answer() { AnsweredAssignmentId = Id };
                return View(answer);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Solve(Answer answer)
        {
            if (answer != null)
            {
                answerRepository.SaveAnswer(answer);
                bool isCorrect = answer.ChosenAnswerOption.AnswerOptionID
                    == answer.AnsweredAssignment
                        .AnswerOptions[answer.AnsweredAssignment.CorrectAnswer].AnswerOptionID ? true : false;

                return RedirectToAction(nameof(AnswerResponse), isCorrect);
            }
            return View(answer);
        }

        public ViewResult AnswerResponse(bool isCorrect)
        {
            return View(isCorrect);
        }*/
    }
}
