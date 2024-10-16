using Microsoft.AspNetCore.Mvc;
using LearnFlow.Models;
using LearnFlow.ViewModel;
using LearnFlow.Interfaces;

namespace LearnFlow.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IVideoService _videoService;

        public LecturesController(ILectureRepository lectureRepository, IVideoService videoService)
        {
            _lectureRepository = lectureRepository;
            _videoService = videoService;
        }

        // GET: Lectures
        public async Task<IActionResult> Index()
        {
            var lectures = await _lectureRepository.GetAllLecturesAsync();
            return View(lectures);
        }

        public async Task<IActionResult> Details(int id)
        {
            var lecture = await _lectureRepository.GetLectureByIdAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }
            return View(lecture);
        }

        // GET: Lectures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lectures/Create
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
                    CourseId = lecture.CourseId
                };
                await _lectureRepository.AddLectureAsync(lectureToAdd);

                return RedirectToAction("Index");
            }

            return View(lecture);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lecture = await _lectureRepository.GetLectureByIdAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }
            return View(lecture);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _lectureRepository.DeleteLectureAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Lectures/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var lecture = await _lectureRepository.GetLectureByIdAsync(id);
            if (lecture == null)
            {
                return NotFound();
            }

            var lectureViewModel = new EditVideoViewModel
            {
                LectureId = lecture.LectureId,
                Title = lecture.Title,
                CourseId = lecture.CourseId,
                CurrentContentUrl = lecture.Content
            };

            return View(lectureViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditVideoViewModel model)
        {
            if (id != model.LectureId) return BadRequest();

            
                var lectureToUpdate = await _lectureRepository.GetLectureByIdAsync(id);
                if (lectureToUpdate == null) return NotFound();

                if (model.NewContent != null)
                {
                    if (!string.IsNullOrEmpty(lectureToUpdate.Content))
                    {
                        await _videoService.DeleteVideoAsync(lectureToUpdate.Content);
                    }

                    var result = await _videoService.AddVideoAsync(model.NewContent);
                    lectureToUpdate.Content = result.Url.ToString();
                }

                lectureToUpdate.Title = model.Title;
                lectureToUpdate.CourseId = model.CourseId;
                

                await _lectureRepository.UpdateLectureAsync(lectureToUpdate);

                return RedirectToAction(nameof(Index));
            
        }

    }
}
