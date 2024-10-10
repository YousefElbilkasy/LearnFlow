using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View("~/Views/Dashboard/AdminDashboard.cshtml");
        }
    }
}
