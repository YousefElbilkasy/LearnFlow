using LearnFlow.Interfaces;
using LearnFlow.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  public class UserController : Controller
  {
    // GET: UserController
    private readonly IUserRepo _userRepo;

    public UserController(IUserRepo userRepo)
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
