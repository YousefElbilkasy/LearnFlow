using System.Drawing;
using System.Security.Claims;
using CloudinaryDotNet.Actions;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<User> UserManager;
    private readonly SignInManager<User> signInManager;
    private readonly IImageService imageService;
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IImageService imageService)
    {
      this.UserManager = userManager;
      this.signInManager = signInManager;
      this.imageService = imageService;
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

      if (ModelState.IsValid)
      {
        // Save Image
        ImageUploadResult uploadResult = null;
        if (model.ImageUrl != null)
        {
          uploadResult = await imageService.AddImageAsync(model.ImageUrl);
        }

        // Save User
        var user = new User
        {
          FullName = model.FullName,
          UserName = model.UserName,
          Email = model.Email,
          PhoneNumber = model.PhoneNumber,
          Role = model.Role,
          DateJoined = DateTime.Now,
          PasswordHash = model.Password
        };

        if (uploadResult != null)
        {
          user.ImageUrl = uploadResult.Url.ToString();
        }

        var result = await UserManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
          await signInManager.SignInAsync(user, false);
          await UserManager.AddToRoleAsync(user, model.Role.ToString());
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

    [HttpPost]
    public async Task<IActionResult> SaveLogIn(LogInUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await UserManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
          var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
          if (result.Succeeded)
          {
            return RedirectToAction("Index", "Home");
          }
        }

        ModelState.AddModelError("", "Invalid Email or Password");
      }
      return View("LogIn", model);
    }

    public async Task<IActionResult> LogOut()
    {
      await signInManager.SignOutAsync();
      return RedirectToAction("LogIn");
    }
  }
}