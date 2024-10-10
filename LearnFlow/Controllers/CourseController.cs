using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  public class CourseController : Controller
  {
    private readonly IRepo<Course> courseRepo;

    public CourseController(IRepo<Course> courseRepo)
    {
      this.courseRepo = courseRepo;
    }

    // GET: CourseController
    public async Task<ActionResult> Index()
    {
      var courses = await courseRepo.GetAllAsync();
      return View(courses);
    }

    // GET: CourseController/Details/5
    public async Task<ActionResult> Details(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // GET: CourseController/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: CourseController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Course course)
    {
      if (ModelState.IsValid)
      {
        await courseRepo.CreateAsync(course);
        return RedirectToAction(nameof(Index));
      }
      return View(course);
    }

    // GET: CourseController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // POST: CourseController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
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
    public async Task<ActionResult> Delete(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      return View(course);
    }

    // POST: CourseController/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      await courseRepo.DeleteAsync(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
