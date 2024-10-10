

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LearnFlow.Data;
using LearnFlow.Models;
using Microsoft.AspNetCore.Identity;
using System;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(ApplicationUser model)
    {
        if (ModelState.IsValid)
        {
            // Check if email already exists in the database
            var existingUser = await _context.RegisterUser.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                // Email already exists, return an error
                ModelState.AddModelError("Email", "Email is already taken.");
                return View(model);
            }

            // Hash password
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            model.PasswordHash = passwordHasher.HashPassword(model, model.PasswordHash);

            // Assign default role and set date of joining
            model.DateJoined = DateTime.Now;

            // Save user to database
            _context.RegisterUser.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        return View(model); // Return the form with validation errors
    }

    // GET: Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.RegisterUser.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    // Check user's role and redirect accordingly
                    switch (user.Role)
                    {
                        case 1: // Instructor
                            return RedirectToAction("InstructorDashboard", "Instructor"); // Redirect to Instructor's dashboard
                        case 2: // Student
                            return RedirectToAction("StudentDashboard", "Student"); // Redirect to Student's dashboard
                        case 3: // Admin
                            return RedirectToAction("AdminDashboard", "Admin"); // Redirect to Admin's dashboard
                        default:
                            return RedirectToAction("Index", "Home"); // Default redirection if role is not recognized
                    }
                }
            }

            // Add error if the user or password is incorrect
            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(model);
    }

    // GET: ForgotPassword
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // POST: ForgotPassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.RegisterUser.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                // Generate password reset token manually
                var token = GeneratePasswordResetToken();

                // Send the token to the user's email (for now just return confirmation view)
                ViewBag.Message = $"A password reset email with a token has been sent to {model.Email}.";

                // TODO: Send the token via email (for example using a third-party email service)

                return View("ForgotPasswordConfirmation");
            }

            ModelState.AddModelError("", "Email address not found.");
        }
        return View();
    }

    // GET: ForgotPasswordConfirmation
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    // Token generation method (manual, simplified)
    private string GeneratePasswordResetToken()
    {
        // Use Guid to generate a simple token (for demo purposes)
        return Guid.NewGuid().ToString();
    }
}