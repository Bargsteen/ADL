using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System;

namespace ADL.Controllers
{
    public class StatisticsController : Controller
    {
        private IAnswerRepository answerRepository;
        public StatisticsController(IAnswerRepository answerRepo)
        {
            answerRepository = answerRepo;
        }
        public ViewResult Index()
        {
            return View(answerRepository.Answers);
        }
    }
}