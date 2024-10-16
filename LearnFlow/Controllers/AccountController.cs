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
            // Save Image
            if (model.ImageUrl != null)
            {
                var fileName =
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\users", model.ImageUrl.FileName);
                model.ImageUrl.CopyTo(new FileStream(fileName, FileMode.Create));
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.FullName,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Role = model.Role,
                    ImageUrl = model.ImageUrl?.FileName ?? "default.png",
                    DateJoined = DateTime.Now,
                    PasswordHash = model.Password
                };

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

        [HttpPost]
        public async Task<IActionResult> SaveLogIn(LogInUserViewModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                var emailfound = await UserManager.FindByEmailAsync(loginmodel.Email);
                if (emailfound != null)
                {
                    bool isPasswordValid = await UserManager.CheckPasswordAsync(emailfound, loginmodel.Password);
                    if (isPasswordValid)
                    {
                        var result = await signInManager.PasswordSignInAsync(emailfound, loginmodel.Password, false, false);
                        if (result.Succeeded) return RedirectToAction("Index", "Home");
                        TempData["Error"] = "Login Failed, please try again later";
                        return View(loginmodel);
                    }
                    TempData["Error"] = "Wrong Credentials, please try again";
                    return View(loginmodel);
                }
                TempData["Error"] = "Email not Found";
                return View(loginmodel);

            }
            return View("LogIn", loginmodel);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }
    }
}