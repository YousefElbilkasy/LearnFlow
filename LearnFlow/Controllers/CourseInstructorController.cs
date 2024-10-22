using System.Security.Claims;
using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repository;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  public class CourseInstructorController : Controller
  {
    private readonly CourseRepo courseRepo;
    private readonly LearnFlowContext context;
    public CourseInstructorController(CourseRepo courseRepo, LearnFlowContext context)
    {
      this.courseRepo = courseRepo;
      this.context = context;
    }

    // GET: CourseInstructor/Create
    public async Task<IActionResult> InstructorDashboard()
    {
      var courses = await courseRepo.GetAllForInstructorAsync(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

      // Convert each Course to CourseViewModel
      var courseViewModels = courses.Select(course => new CourseViewModel
      {
        CourseId = course.CourseId,
        Title = course.Title,
        Description = course.Description,
        Price = course.Price,
        // Add other necessary properties here
      }).ToList();

      return View("InstructorDashboard", courseViewModels);
    }

    [Authorize(Roles = "Instructor")]  
    public ActionResult Create()
    {
      return View();
    }

    [Authorize(Roles = "Instructor")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CourseViewModel model)
    {
      if (ModelState.IsValid)
      {
        var course = new Course
        {
          Title = model.Title,
          Description = model.Description,
          Price = model.Price,
          InstructorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
          CreationDate = DateTime.Now,
          IsVerified = false
        };

        await courseRepo.CreateAsync(course);
        return RedirectToAction("InstructorDashboard");
      }
      return View(model);
    }

    [Authorize(Roles = "Instructor")]
    // GET: CourseInstructorController/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      if (course == null)
      {
        return NotFound();
      }

      // Map the Course to CourseViewModel
      var courseViewModel = new CourseViewModel
      {
        CourseId = course.CourseId,
        Title = course.Title,
        Description = course.Description,
        Price = course.Price
        // Add any other fields you need
      };

      return View(courseViewModel);
    }

    // POST: CourseInstructorController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseViewModel model)
    {
      if (ModelState.IsValid)
      {
        var course = await courseRepo.GetByIdAsync(id);
        if (course == null)
        {
          return NotFound();
        }

        // Map CourseViewModel back to Course entity
        course.Title = model.Title;
        course.Description = model.Description;
        course.Price = model.Price;

        await courseRepo.UpdateAsync(course);
        return RedirectToAction("InstructorDashboard");
      }

      return View(model);
    }

    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult> Delete(int id)
    {
      var course = await GetCourseByIdAsync(id);
      if (course == null)
      {
        return NotFound();
      }

      // Show confirmation page with course details (or skip to POST directly)
      var courseViewModel = new CourseViewModel
      {
        CourseId = course.CourseId,
        Title = course.Title,
        Description = course.Description,
        Price = course.Price
        // Add any other fields you need
      };

      return View(courseViewModel);
    }

    [Authorize (Roles = "Instructor")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      var course = await GetCourseByIdAsync(id);  // Fetch course by id
      if (course == null)
      {
        return NotFound();
      }

      // Delete the course from the repository
      await courseRepo.DeleteAsync(id);

      // Redirect to the Instructor Dashboard after deletion
      return RedirectToAction("InstructorDashboard", "CourseInstructor");
    }

    // Helper method to validate course ownership
    private async Task<Course> GetCourseByIdAsync(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      if (course == null)
      {
        return null;
      }

      // var loggedInInstructorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      // if (course.InstructorId != int.Parse(loggedInInstructorId))
      // {
      //   return null;
      // }

      return course;
    }
  }
}