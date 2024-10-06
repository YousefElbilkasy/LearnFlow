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

    public IActionResult Index()
    {
      var users = _userRepo.GetAllUsers();
      return View(users);
    }

    public IActionResult ViewUser(int id)
    {
      var user = _userRepo.GetUserById(id);
      return View(user);
    }
  }

}
