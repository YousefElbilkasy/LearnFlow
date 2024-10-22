using LearnFlow.Models;
using LearnFlow.Repository;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  public class EnrollmentController : Controller
  {
    private readonly CourseRepo courseRepo;
    private readonly IEnrollmentRepo enrollmentRepo;
    private readonly UserManager<User> userManager;

    public EnrollmentController(CourseRepo courseRepo, IEnrollmentRepo enrollmentRepo, UserManager<User> userManager)
    {
      this.courseRepo = courseRepo;
      this.enrollmentRepo = enrollmentRepo;
      this.userManager = userManager;
    }

    [Authorize(Roles = "Student")]
    [HttpGet]
    // GET: EnrollmentController/Enroll/id
    public async Task<ActionResult> Enroll(int id)
    {
      // Get the course to enroll in
      var course = await courseRepo.GetByIdForEnrollAsync(id);

      // Get the current user
      var user = await userManager.GetUserAsync(User);

      // Create the view model
      var enrollViewModel = new EnrollViewModel
      {
        Course = new CourseEnrollViewModel
        {
          CourseId = course.CourseId,
          Title = course.Title,
          Description = course.Description,
          Price = course.Price,
          ImageUrl = course.ImageUrl,
          InstructorId = course.InstructorId,
          CreationDate = course.CreationDate
        },
        Email = await userManager.GetEmailAsync(user),
        FullName = await userManager.GetUserNameAsync(user),
        PhoneNumber = await userManager.GetPhoneNumberAsync(user)
      };
      return View(enrollViewModel);
    }

    [Authorize(Roles = "Student")]
    [HttpPost]
    // POST: EnrollmentController/Enroll
    public async Task<ActionResult> Enroll(EnrollViewModel enrollViewModel)
    {
      if (ModelState.IsValid)
      {
        var student = await userManager.GetUserAsync(User);
        var enrollment = new Enrollment
        {
          StudentId = student.Id,
          CourseId = enrollViewModel.Course.CourseId,
          EnrollmentDate = DateTime.Now
        };
        await enrollmentRepo.EnrollStudentAsync(enrollment);
        return RedirectToAction("Index", "Course");
      }
      return View(enrollViewModel);
    }
  }
}
