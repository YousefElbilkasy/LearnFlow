using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentDashboard()
        {
            return View("~/Views/Dashboard/StudentDashboard.cshtml");
        }
    }
}
