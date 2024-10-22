using LearnFlow.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
  [Authorize(Roles = "Student")]
  public class StudentController : Controller
  {
    private readonly IStudentRepo _studentRepo;

    public StudentController(IStudentRepo studentRepo)
    {
      _studentRepo = studentRepo;
    }

    [HttpGet]
    public async Task<IActionResult> StudentIndex(int id)
    {
      var student = await _studentRepo.GetStudentById(id);
      if (student == null)
        return NotFound();

      var viewModel = new
      {
        Student = student
      };

      return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int studentId)
    {
      var enrollments = await _studentRepo.GetEnrollmentsByStudentId(studentId);
      return View(enrollments);
    }
    [HttpGet]
    public async Task<IActionResult> Result(int studentId)
    {
      var quizResults = await _studentRepo.GetQuizResultsByStudentId(studentId);
      return View(quizResults);
    }
  }
}
