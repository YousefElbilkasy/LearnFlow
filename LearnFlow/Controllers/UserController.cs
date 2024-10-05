using LearnFlow.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  public class UserController : Controller
  {
    // GET: UserController
    private readonly UserRepo _userRepo;

    public UserController(UserRepo userRepo)
    {
      _userRepo = userRepo;
    }

    public ActionResult Index()
    {
      var users = _userRepo.GetAllUsers();
      return View(users);
    }

  }
}
