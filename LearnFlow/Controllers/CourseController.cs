using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repository;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LearnFlow.Data;

namespace LearnFlow.Controllers
{
  public class CourseController : Controller
  {
    private readonly CourseRepo courseRepo;
    private readonly UserManager<User> userManager;
    private readonly IRepo<Lecture> lectureRepo;
    private readonly IRepo<Quiz> quizRepo;
    private readonly IRepo<Question> questionRepo;
    private readonly Cloudinary cloudinary;
    private readonly UploadToCloudinaryRepo uploadToCloudinaryRepo;
    private readonly LearnFlowContext dbContext;
    private readonly IEnrollmentRepo enrollmentRepo;

    public CourseController(CourseRepo courseRepo, IEnrollmentRepo enrollmentRepo, UserManager<User> userManager, IRepo<Lecture> lectureRepo, IRepo<Quiz> quizRepo, IRepo<Question> questionRepo, Cloudinary cloudinary, UploadToCloudinaryRepo uploadToCloudinaryRepo, LearnFlowContext dbContext)
    {
      this.courseRepo = courseRepo;
      this.userManager = userManager;
      this.lectureRepo = lectureRepo;
      this.quizRepo = quizRepo;
      this.questionRepo = questionRepo;
      this.cloudinary = cloudinary;
      this.uploadToCloudinaryRepo = uploadToCloudinaryRepo;
      this.dbContext = dbContext;
      this.enrollmentRepo = enrollmentRepo;
    }

    // GET: CourseController
    public async Task<ActionResult> Index()
    {
      var courses = await courseRepo.GetAllForIsVerifiedAsync();
      return View(courses);
    }

    // GET: CourseController/Details/3
    public async Task<ActionResult> Details(int id, int? selectedLectureId)
    {
      var course = await courseRepo.GetByIdAsync(id);
      if (course == null)
      {
        return NotFound();
      }

      // Check if the user is enrolled in the course
      if (course.Enrollments.Any(e => e.StudentId.ToString() == User.FindFirstValue(ClaimTypes.NameIdentifier)))
      {
        var displayCourse = new DisplayCourseViewModel
        {
          CourseId = course.CourseId,
          CourseTitle = course.Title,
          CourseDescription = course.Description,
          CourseInstructor = course.Instructor,
          Lectures = course.Lectures.Select(l => new DisplayLectureViewModel
          {
            LectureId = l.LectureId,
            LectureTitle = l.Title,
            LectureVideoUrl = l.Content,
          }).ToList()
        };

        // Select the lecture, if not specified, default to the first lecture
        displayCourse.SelectedLecture = selectedLectureId.HasValue
            ? displayCourse.Lectures.FirstOrDefault(l => l.LectureId == selectedLectureId.Value)
            : displayCourse.Lectures.First();
        return View("DisplayCourse", displayCourse);
      }
      return View(course);
    }

    [Authorize(Roles = "Instructor, Admin")]
    public async Task<IActionResult> CourseDetails(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // GET: CourseController/Edit/5
    [Authorize(Roles = "Instructor, Admin")]
    public async Task<ActionResult> Edit(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // POST: CourseController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Instructor, Admin")]
    public async Task<ActionResult> Edit(int id, Course course)
    {
      if (id != course.CourseId)
      {
        return NotFound();
      }
      if (ModelState.IsValid)
      {
        await courseRepo.UpdateAsync(course);
        return RedirectToAction(nameof(Index));
      }
      return View(course);
    }

    // GET: CourseController/Delete/5
    [Authorize(Roles = "Instructor, Admin")]
    public async Task<ActionResult> Delete(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // POST: CourseController/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Instructor, Admin")]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      await courseRepo.DeleteAsync(id);
      return RedirectToAction(nameof(Index));
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<ActionResult> Enroll(int id)
    // {

    //   int studentId = 1;


    //   var existingEnrollment = await enrollmentRepo.GetEnrollmentByStudentAndCourseAsync(studentId, id);

    //   if (existingEnrollment == null)
    //   {

    //     var enrollment = new Enrollment
    //     {
    //       CourseId = id,
    //       StudentId = studentId,
    //       EnrollmentDate = DateTime.Now,
    //       Progress = 0
    //     };

    //     await enrollmentRepo.EnrollStudentAsync(enrollment);


    //     TempData["SuccessMessage"] = "You have successfully enrolled in the course!";
    //   }
    //   else
    //   {

    //     TempData["SuccessMessage"] = "You are already enrolled in this course.";
    //   }

    //   return RedirectToAction(nameof(Details), new { id });
    // }

    [Authorize(Roles = "Student")]
    [HttpGet]
    public async Task<ActionResult> MyCourses()
    {
      var user = await userManager.GetUserAsync(User);
      var courses = await courseRepo.GetAllForStudentAsync(user.Id);
      return View(courses);
    }
  }
}
