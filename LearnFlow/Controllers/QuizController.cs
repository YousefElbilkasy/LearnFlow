using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
    public class QuizController : Controller
    {
        private readonly IRepo<Quiz> _quizRepo;

        public QuizController(IRepo<Quiz> quizRepo)
        {
            _quizRepo = quizRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allQuizs = await _quizRepo.GetAllAsync();
            return View(allQuizs);
        }
        [HttpGet]
        public async Task<IActionResult> ViewQuiz(int quizId)
        {
            var quiz = await _quizRepo.GetByIdAsync(quizId);
            return View(quiz);
        }

    }
}
