
using Microsoft.AspNetCore.Mvc;
using LearnFlow.Models;
using System.Threading.Tasks;
using LearnFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
  private readonly LearnFlowContext _context;

  public AdminController(LearnFlowContext context)
  {
    _context = context;
  }


  // GET: List all instructors
  public async Task<IActionResult> Instructors()
  {

    var instructors = await _context.Users
    .Where(u => u.Role == UserRole.Instructor)
    .ToListAsync();
    return View(instructors);
  }

  // GET: Edit an instructor 
  public async Task<IActionResult> EditInstructor(int id)
  {
    var instructor = await _context.Users.FindAsync(id);
    if (instructor == null) return NotFound();

    return View(instructor);
  }


  [HttpPost]
  public async Task<IActionResult> EditInstructor(User instructor)
  {
    if (ModelState.IsValid)
    {
      var existingInstructor = await _context.Users.FindAsync(instructor.Id);
      if (existingInstructor == null)
      {
        return NotFound();
      }


      existingInstructor.FullName = instructor.FullName;
      existingInstructor.Email = instructor.Email;
      existingInstructor.Role = instructor.Role;


      _context.Users.Update(existingInstructor);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Instructors));
    }


    return View(instructor);
  }



  public async Task<IActionResult> VerifyCourses()
  {

    var unverifiedCourses = await _context.Courses
        .Where(c => !c.IsVerified)
        .ToListAsync();

    return View(unverifiedCourses);
  }

  [HttpPost]
  public async Task<IActionResult> VerifyCourse(int id)
  {
    var course = await _context.Courses.FindAsync(id);
    if (course == null) return NotFound();


    course.IsVerified = true;

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(VerifyCourses));
  }
  public async Task<IActionResult> DeleteInstructor(int id)
  {
    var instructor = await _context.Users.FindAsync(id);
    if (instructor == null)
    {
      return NotFound();
    }
    return View(instructor); // Passes the instructor to the view for confirmation
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteInstructorConfirmed(int id)
  {
    var instructor = await _context.Users.FindAsync(id);
    if (instructor == null)
    {
      return NotFound();
    }

    _context.Users.Remove(instructor);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Instructors)); // Redirects back to the list after deletion
  }
}