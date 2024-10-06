using LearnFlow.Interfaces;
using LearnFlow.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LearnFlow.Controllers
{
	public class QuizController : Controller
	{
		private readonly IQuizRepo _quizRepo;

		public QuizController(IQuizRepo quizRepo)
		{
			_quizRepo = quizRepo;
		}

		public async Task<IActionResult> Index()
		{
			var quizes = await _quizRepo.GetAllQuizes();
			return View(quizes);
		}

		public async Task<IActionResult> Details(int id)
		{
			var quiz = await _quizRepo.GetQuizById(id);
			return View(quiz);
		}
	}
}
