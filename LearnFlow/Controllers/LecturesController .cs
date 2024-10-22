using Microsoft.AspNetCore.Mvc;
using LearnFlow.Models;
using LearnFlow.ViewModel;
using LearnFlow.Interfaces;
using Microsoft.AspNetCore.Authorization;
using LearnFlow.Data;
using Microsoft.Extensions.Logging; // Ensure this is included for logging
using System.Threading.Tasks;
using System.Linq;

public class LecturesController : Controller
{
  private readonly ILectureRepository _lectureRepository;
  private readonly IVideoService _videoService;
  private readonly LearnFlowContext _context;  // Inject LearnFlowContext
  private readonly ILogger<LecturesController> _logger; // Logger for debugging

  public LecturesController(ILectureRepository lectureRepository, IVideoService videoService, LearnFlowContext context, ILogger<LecturesController> logger)
  {
    _lectureRepository = lectureRepository;
    _videoService = videoService;
    _context = context;  // Assign context
    _logger = logger; // Assign logger
  }


  [HttpGet]
  public async Task<IActionResult> Index(int courseId)
  {
    _logger.LogInformation("Retrieving lectures for Course ID: {CourseId}", courseId);

    // Retrieve the lectures for the given course ID
    var lectures = await _lectureRepository.GetLecturesByCourseIdAsync(courseId);

    if (lectures == null || !lectures.Any())
    {
      ViewBag.Message = "No lectures available for this course.";
      ViewData["CourseId"] = courseId; // Ensure course ID is set
      return View(); // Return to the same view with a message
    }

    // Retrieve the course from the context by courseId
    var course = await _context.Courses.FindAsync(courseId);
    if (course == null)
    {
      return NotFound("Course not found.");
    }

    ViewData["CourseName"] = course.Title;  // Set the course name
    ViewData["CourseId"] = courseId;

    // Set the course ID for back button

    return View(lectures);
  }
  public async Task<IActionResult> Details(int id, int courseId)
  {
    _logger.LogInformation("Retrieving lecture with ID: {LectureId} for Course ID: {CourseId}", id, courseId);


    var lecture = await _lectureRepository.GetLectureByIdAsync(id);


    if (lecture == null)
    {
      return NotFound();
    }


    ViewData["CourseId"] = courseId;

    // Pass the specific lecture to the view
    return View(lecture);
  }
  public IActionResult Create(int courseId)
  {
    ViewData["CourseId"] = courseId;
    return View(new CreateVideoViewModel { CourseId = courseId });
  }

  [Authorize(Roles = "Instructor")]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(CreateVideoViewModel lecture)
  {
    if (ModelState.IsValid)
    {
        var result = await _videoService.AddVideoAsync(lecture.Content);
        var lectureToAdd = new Lecture
        {
            Title = lecture.Title,
            Content = result.Url.ToString(),
            CourseId = lecture.CourseId,
            Order=lecture.Order
        };

      await _lectureRepository.AddLectureAsync(lectureToAdd);

      // Redirect back to the Index action with the courseId
      return RedirectToAction("Index", "Lectures", new { courseId = lecture.CourseId });
    }

    // If model is invalid, return to the same view with validation errors
    ViewData["CourseId"] = lecture.CourseId; // Ensure the course ID is set again if there are errors
    return View(lecture);
  }



  // GET: Lectures/Delete/5
  public async Task<IActionResult> Delete(int id)
  {
    var lecture = await _lectureRepository.GetLectureByIdAsync(id);
    if (lecture == null)
    {
      return NotFound();
    }

    // Assuming that `lecture.CourseId` holds the associated course's ID
    ViewData["CourseId"] = lecture.CourseId;

    return View(lecture);
  }


  // POST: Lectures/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    var lecture = await _lectureRepository.GetLectureByIdAsync(id);
    if (lecture == null)
    {
      return NotFound();
    }

    int courseId = lecture.CourseId; // Store courseId before deleting the lecture

    await _lectureRepository.DeleteLectureAsync(id);

    // Redirect to Index with the courseId to show the correct list
    return RedirectToAction(nameof(Index), new { courseId = courseId });
  }

public async Task<IActionResult> Edit(int id)
{
    var lecture = await _lectureRepository.GetLectureByIdAsync(id);
    if (lecture == null)
    {
      return NotFound();
    }

    // Populate the ViewModel, including CourseId and Order
    var lectureViewModel = new EditVideoViewModel
    {
        LectureId = lecture.LectureId,
        Title = lecture.Title,
        CourseId = lecture.CourseId,
        CurrentContentUrl = lecture.Content,
        Order = lecture.Order  // Populate Order
    };

    ViewData["CourseId"] = lecture.CourseId;

    return View(lectureViewModel);
  }


  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, EditVideoViewModel model)
  {
    if (id != model.LectureId)
    {
      return BadRequest();
    }

    var lectureToUpdate = await _lectureRepository.GetLectureByIdAsync(id);
    if (lectureToUpdate == null)
    {
      return NotFound();
    }

    // If the content is updated, upload the new video
    if (model.NewContent != null)
    {
      // Delete the existing video
      if (!string.IsNullOrEmpty(lectureToUpdate.Content))
      {
        await _videoService.DeleteVideoAsync(lectureToUpdate.Content);
      }

      // Add the new video
      var result = await _videoService.AddVideoAsync(model.NewContent);
      lectureToUpdate.Content = result.Url.ToString();
    }

    // Update title, course, and order
    lectureToUpdate.Title = model.Title;
    lectureToUpdate.CourseId = model.CourseId;
    lectureToUpdate.Order = model.Order;  // Update Order

    await _lectureRepository.UpdateLectureAsync(lectureToUpdate);

    // Redirect back to Index, passing courseId
    return RedirectToAction(nameof(Index), new { courseId = model.CourseId });
  }


}
