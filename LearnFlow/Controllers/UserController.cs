using LearnFlow.Models;
using LearnFlow.Repository;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LearnFlow.Controllers
{
  public class UserController : Controller
  {
    public readonly RoleManager<IdentityRole<int>> role;
    public readonly UserManager<User> userManager;

    public UserController(RoleManager<IdentityRole<int>> role, UserManager<User> userManager)
    {
      this.role = role;
      this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> ViewProfile(int id)
    {
      var user = await userManager.FindByIdAsync(id.ToString());
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

      userProfile.ImageUrl = "/uploads/" + userProfile.ImageUrl;

      if (string.IsNullOrEmpty(userProfile.ImageUrl))
        userProfile.ImageUrl = "default.png";

      return View(userProfile);
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile(int id)
    {
      var user = await userManager.FindByIdAsync(id.ToString());
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

      userProfile.ImageUrl = "/uploads/" + userProfile.ImageUrl;

      if (userProfile.ImageUrl == null)
        userProfile.ImageUrl += "default.png";

      return View(userProfile);
    }

    [HttpPost]
    public async Task<IActionResult> SaveEditProfile(EditProfileViewModel model)
    {
      if (ModelState.IsValid)
      {
        // Find user by id
        var user = await userManager.FindByIdAsync(model.Id.ToString());

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
          var fileName =
          Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", model.NewImageUrl.FileName);
          model.NewImageUrl.CopyTo(new FileStream(fileName, FileMode.Create));

          // Delete old image
          if (user.ImageUrl != "default.png")
          {
            var oldImagePath =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", user.ImageUrl);
            if (System.IO.File.Exists(oldImagePath))
              System.IO.File.Delete(oldImagePath);
          }

          // Update user image in database
          user.ImageUrl = model.NewImageUrl.FileName;
        }
        // Save changes
        await userManager.UpdateAsync(user);

        // Redirect to view profile
        return RedirectToAction("ViewProfile", new { id = user.Id });
      }

      // Return to edit profile view
      return View("EditProfile", model);
    }
  }
}
