using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizRepo _quizRepo;

        public QuizController(IQuizRepo quizRepo)
        {
            _quizRepo = quizRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allQuizs = await _quizRepo.GetAllQuizes();
            return View(allQuizs);
        }
        [HttpGet]
        public async Task<IActionResult> ViewQuiz(int quizId)
        {
            var quiz = await _quizRepo.GetQuiz(quizId);
            return View(quiz);
        }

    }
}
