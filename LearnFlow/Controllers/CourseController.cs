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
using LearnFlow.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnFlow.Controllers
{
  public class CourseController : Controller
  {
    private readonly CourseRepo courseRepo;
    private readonly UserManager<User> userManager;
    private readonly Cloudinary cloudinary;
    private readonly IEnrollmentRepo enrollmentRepo;
    private readonly LearnFlowContext _context;

    public CourseController(CourseRepo courseRepo, IEnrollmentRepo enrollmentRepo, UserManager<User> userManager, Cloudinary cloudinary, LearnFlowContext context)
    {
      this.courseRepo = courseRepo;
      this.userManager = userManager;
      this.cloudinary = cloudinary;
      this.enrollmentRepo = enrollmentRepo;
      _context = context;
    }

    // GET: CourseController
    public async Task<ActionResult> Index()
    {
      var courses = await courseRepo.GetAllForIsVerifiedAsync();
      return View(courses);
    }

    // GET: CourseController/Details/3
    [Authorize(Roles = "Student, Instructor")]
    public async Task<IActionResult> Details(int id, int? selectedLectureId)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var enrollment = await _context.Enrollments
        .FirstOrDefaultAsync(e => e.StudentId.ToString() == userId && e.CourseId == id);

      if (enrollment == null)
      {
        var ViewCourse = await courseRepo.GetByIdAsync(id);
        return View(ViewCourse);
      }

      var course = await _context.Courses
        .Include(c => c.Lectures)
        .FirstOrDefaultAsync(c => c.CourseId == id);

      if (course == null)
      {
        return NotFound();
      }

      var viewModel = new DisplayCourseViewModel
      {
        CourseId = course.CourseId,
        CourseTitle = course.Title,
        CourseDescription = course.Description,
        Lectures = course.Lectures.Select(l => new DisplayLectureViewModel
        {
          LectureId = l.LectureId,
          LectureTitle = l.Title,
          LectureVideoUrl = l.Content
        }).ToList(),
        SelectedLecture = selectedLectureId.HasValue
        ? course.Lectures.Select(l => new DisplayLectureViewModel
        {
          LectureId = l.LectureId,
          LectureTitle = l.Title,
          LectureVideoUrl = l.Content
        }).FirstOrDefault(l => l.LectureId == selectedLectureId.Value)
        : null,
        Progress = enrollment.Progress // Pass the progress value
      };

      return View("DisplayCourse", viewModel);
    }

    [Authorize(Roles = "Student")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsCompleted(int CompletedLectureId, int courseId)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.StudentId.ToString() == userId && e.CourseId == courseId);

      if (enrollment == null)
      {
        return NotFound();
      }

      var progress = await _context.Progresses.FirstOrDefaultAsync(p => p.EnrollmentId == enrollment.EnrollmentId && p.LectureId == CompletedLectureId);

      if (progress == null)
      {
        progress = new Progress
        {
          EnrollmentId = enrollment.EnrollmentId,
          LectureId = CompletedLectureId,
          IsCompleted = true
        };
        await _context.Progresses.AddAsync(progress);
      }
      else
      {
        progress.IsCompleted = true;
        _context.Progresses.Update(progress);
      }

      await _context.SaveChangesAsync();

      var totalLectures = await _context.Lectures.CountAsync(l => l.CourseId == courseId);
      var completedLectures = await _context.Progresses.CountAsync(p => p.EnrollmentId == enrollment.EnrollmentId && p.IsCompleted);

      enrollment.Progress = (completedLectures / (float)totalLectures) * 100;
      _context.Enrollments.Update(enrollment);
      await _context.SaveChangesAsync();

      // Get the next lecture
      var nextLecture = await _context.Lectures
          .Where(l => l.CourseId == courseId && l.Order > _context.Lectures.FirstOrDefault(l => l.LectureId == CompletedLectureId).Order)
          .OrderBy(l => l.Order)
          .FirstOrDefaultAsync();

      if (nextLecture == null)
      {
        // If there is no next lecture, redirect to the course details page
        return RedirectToAction("Details", new { Id = courseId });
      }

      // Redirect to the course details page with the next lecture selected
      return RedirectToAction("Details", new { Id = courseId, selectedLectureId = nextLecture.LectureId });
    }

    [Authorize(Roles = "Instructor, Admin")]
    public async Task<IActionResult> CourseDetails(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // GET: CourseController/Edit/5
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult> Edit(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // POST: CourseController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Instructor")]
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
    [Authorize(Roles = "Instructor")]
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
    public async Task<IActionResult> MyCourses()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var enrollments = await _context.Enrollments
          .Include(e => e.Course)
          .Where(e => e.StudentId.ToString() == userId)
          .ToListAsync();

      var viewModel = enrollments.Select(e => new CourseWithProgressViewModel
      {
        CourseId = e.Course.CourseId,
        Title = e.Course.Title,
        ImageUrl = e.Course.ImageUrl,
        Progress = e.Progress // Pass the progress value
      }).ToList();

      return View(viewModel);
    }

    [HttpGet]
    public JsonResult GetCourses(string search)
    {
      if (string.IsNullOrEmpty(search))
        return Json(new List<Course>()); // ???? ?? ??? ????? null

      var courses = courseRepo.SearchCourses(search)
          .Select(c => new
          {
            courseId = c.CourseId,
            title = c.Title,
            description = c.Description,
            image = string.IsNullOrEmpty(c.ImageUrl) ? "/images/default.png" : c.ImageUrl
          }).ToList();

      return Json(courses);
    }

  }
}
