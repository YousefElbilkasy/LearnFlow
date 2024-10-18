using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repository;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LearnFlow.Controllers
{
  [Authorize]
  public class UserController : Controller
  {
    private readonly RoleManager<IdentityRole<int>> role;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepo _userRepo;
    private readonly IImageService imageService;
    public UserController(RoleManager<IdentityRole<int>> role, UserManager<User> userManager, IUserRepo userRepo, IImageService imageService)
    {
      this.role = role;
      this._userManager = userManager;
      this._userRepo = userRepo;
      this.imageService = imageService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var allUsers = await _userRepo.GetAllUsers();
      return View(allUsers);
    }

    [HttpGet]
    public async Task<IActionResult> ViewProfile(int id)
    {
      var user = await _userManager.FindByIdAsync(id.ToString());
      if (user == null)
        return NotFound();

      var userProfile = new ViewProfileViewModel
      {
        Id = user.Id,
        Email = user.Email,
        FullName = user.FullName,
        ImageUrl = user.ImageUrl,
        UserName = user.UserName,
        PhoneNumber = user.PhoneNumber,
        DateJoined = user.DateJoined
      };

      return View(userProfile);
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile(int id)
    {
      var user = await _userManager.FindByIdAsync(id.ToString());
      if (user == null)
        return NotFound();

      var userProfile = new EditProfileViewModel
      {
        Id = user.Id,
        Email = user.Email,
        FullName = user.FullName,
        ImageUrl = user.ImageUrl,
        UserName = user.UserName,
        PhoneNumber = user.PhoneNumber
      };


      return View(userProfile);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEditProfile(EditProfileViewModel model)
    {
      if (ModelState.IsValid)
      {
        // Find user by id
        var user = await _userManager.FindByIdAsync(model.Id.ToString());

        // Check if user exists
        if (user == null)
          return NotFound();

        // Update user information
        user.Email = model.Email;
        user.FullName = model.FullName;
        user.UserName = model.UserName;
        user.PhoneNumber = model.PhoneNumber;

        // Check if new image is uploaded
        if (model.NewImageUrl != null)
        {
          // Save new image
          var uploadResult = await imageService.AddImageAsync(model.NewImageUrl);

          // Check if user has an image exept default image
          if (user.ImageUrl != "https://res.cloudinary.com/dwwuefmsb/image/upload/v1729191590/default_d1m7rd.png")
            // Delete old image
            await imageService.DeleteImageAsync(user.ImageUrl);

          // Update user image in database
          user.ImageUrl = uploadResult.Url.ToString();
        }
        // Save changes
        await _userManager.UpdateAsync(user);

        // Redirect to view profile
        return RedirectToAction("ViewProfile", new { id = user.Id });
      }

      // Return to edit profile view
      return View("EditProfile", model);
    }

  }
}
