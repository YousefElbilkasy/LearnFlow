using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
