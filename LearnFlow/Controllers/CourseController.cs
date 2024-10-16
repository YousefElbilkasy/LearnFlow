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

    public async Task<ActionResult> Details(int id)
    {
      var course = await courseRepo.GetByIdAsync(id);
      if (course == null)
      {
        return NotFound();
      }
      return View(course);
    }

    // GET: CourseController/Create
    [Authorize(Roles = "Instructor")]
    public ActionResult Create()
    {
      return View();
    }

    // POST: CourseController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Instructor")]
    public async Task<ActionResult> Create(CourseViewModel courseVM)
    {
      // check of model is valid
      if (ModelState.IsValid)
      {
        using (var transaction = await dbContext.Database.BeginTransactionAsync())
        {
          try
          {
            // assign values to course
            var course = new Course
            {
              InstructorId = courseVM.InstructorId, // Current instructor user ID
              Title = courseVM.Title,
              Description = courseVM.Description,
              Price = courseVM.Price,
              ImageUrl = courseVM.ImageUrl ?? "default-course.png"
            };

            // save course to database
            await courseRepo.CreateAsync(course);

            // Commit the transaction
            await transaction.CommitAsync();

            // Redirect to AddLectures with courseId
            return RedirectToAction("AddLectures", new { courseId = course.CourseId });
          }

          catch
          {
            await transaction.RollbackAsync();
            throw;
          }
        }
      }

      // if model is not valid, return to the create view with the model
      return View(courseVM);
    }

    public IActionResult AddLectures(int courseId)
    {
      var model = new AddLecturesViewModel
      {
        CourseId = courseId,
        Lectures = new List<LectureViewModel>()
      };

      return View(model);
    }

    [Authorize(Roles = "Instructor")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddLectures(AddLecturesViewModel model)
    {
      if (ModelState.IsValid)
      {
        using (var transaction = await dbContext.Database.BeginTransactionAsync())
        {
          try
          {
            foreach (var lecture in model.Lectures)
            {
              var newLecture = new Lecture
              {
                CourseId = model.CourseId,
                Title = lecture.Title,
                Content = await uploadToCloudinaryRepo.UploadFileToCloudinary(lecture.Content)
              };

              await lectureRepo.CreateAsync(newLecture);
            }

            return RedirectToAction("CourseDetails", new { id = model.CourseId });
          }

          catch
          {
            await transaction.RollbackAsync();
            throw;
          }
        }
      }

      return View(model);
    }

    // For creating a quiz
    public IActionResult CreateQuiz(int courseId)
    {
      var model = new CreateQuizViewModel { CourseId = courseId };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateQuiz(CreateQuizViewModel model)
    {
      if (ModelState.IsValid)
      {
        var quiz = new Quiz
        {
          CourseId = model.CourseId,
          Title = model.Title,
          MaxScore = model.MaxScore
        };

        await quizRepo.CreateAsync(quiz);
        return RedirectToAction("AddQuestions", new { quizId = quiz.QuizId });
      }

      return View(model);
    }

    // For adding questions to the quiz
    public IActionResult AddQuestions(int quizId)
    {
      var model = new AddQuestionsViewModel { QuizId = quizId };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddQuestions(AddQuestionsViewModel model)
    {
      if (ModelState.IsValid)
      {
        foreach (var question in model.Questions)
        {
          var newQuestion = new Question
          {
            QuizId = model.QuizId,
            Text = question.Text
          };

          await questionRepo.CreateAsync(newQuestion);
        }
        return RedirectToAction("CourseDetails", new { id = model.CourseId });
      }

      return View(model);
    }

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
  }
}
