using System.Drawing;
using LearnFlow.Models;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  public class AccountController : Controller
  {
    public readonly UserManager<User> UserManager;
    public readonly SignInManager<User> signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
      this.UserManager = userManager;
      this.signInManager = signInManager;
    }

    // GET: AccountController
    [HttpGet]
    public ActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> SaveRegister(RegisterUserViewModel model)
    {
      if (model.ImageUrl != null)
      {
        var fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", model.ImageUrl.FileName);
        model.ImageUrl.CopyTo(new FileStream(fileName, FileMode.Create));
      }

      if (ModelState.IsValid)
      {
        var user = new User
        {
          FullName = model.FullName,
          UserName = model.UserName,
          Email = model.Email,
          PhoneNumber = model.PhoneNumber
        };
        user.ImageUrl = model.ImageUrl?.FileName ?? "default.png";

        var result = await UserManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
          await signInManager.SignInAsync(user, false);
          return RedirectToAction("LogIn");
        }

        foreach (var error in result.Errors)
          ModelState.AddModelError("", error.Description);
      }
      return View("Register", model);
    }

    [HttpGet]
    public IActionResult LogIn()
    {
      return View();
    }
  }
}
