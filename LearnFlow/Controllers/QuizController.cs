using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repository;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
    public class QuizController : Controller
    {
        private readonly IRepo<Quiz> _quizRepo;
        private readonly IRepo<Course> _courseRepo;

        public QuizController(IRepo<Quiz> quizRepo, IRepo<Course> courseRepo)
        {
            _quizRepo = quizRepo;
            _courseRepo = courseRepo;
        }

        //Index function
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allQuizs = await _quizRepo.GetAllAsync();
            return View(allQuizs);
        }
        //View Quiz Function
        [HttpGet]
        public async Task<IActionResult> ViewQuiz(int quizId)
        {
            var quiz = await _quizRepo.GetByIdAsync(quizId);
            return View(quiz);
        }

        //Create Functions
        [Authorize(Roles = "Instructor, Admin")]
        public async Task<IActionResult> Create(int courseId)
        {
            var quizVM = new QuizViewModel() { Title = string.Empty };
            quizVM.CourseId = courseId;
            return View(quizVM);
        }

        [Authorize(Roles = "Instructor, Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(QuizViewModel quizViewModel)
        {
            if (ModelState.IsValid)
            {
                var Quiz = new Quiz
                {
                    Title = quizViewModel.Title,
                    MaxScore = quizViewModel.MaxScore,
                    CourseId = quizViewModel.CourseId,
                    Questions = (ICollection<Question>)quizViewModel.Questions.Select(q => new Question
                    {
                        Text = q.Text,
                        Answers = (ICollection<Answer>)q.AnswerOptions.Select(a => new Answer
                        {
                            OptionText = a.OptionText,
                            IsCorrect = a.IsCorrect
                        }).ToList()

                    }).ToList()

                };
                await _quizRepo.CreateAsync(Quiz);

                return RedirectToAction("Index");
            }
            return View("Create",quizViewModel);
        }

        //Delete Functions
        [Authorize(Roles ="Instructor, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var delQuiz = await _quizRepo.GetByIdAsync(id);
            return View(delQuiz);
        }

        [Authorize(Roles = "Instructor, Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteQuiz(int quizId)
        {
            var delQuiz = await _quizRepo.DeleteAsync(quizId);
            if (delQuiz != null)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //Update Functions
        [Authorize(Roles ="Instructor, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var updQuiz = await _quizRepo.GetByIdAsync(id);
            return View(updQuiz);
        }

        [Authorize(Roles = "Instructor, Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(QuizViewModel quizViewModel)
        {
            if (ModelState.IsValid)
            {
                var Quiz = new Quiz()
                {
                    Title = quizViewModel.Title,
                    MaxScore = quizViewModel.MaxScore,
                    CourseId = quizViewModel.CourseId,
                };
                return RedirectToAction("Index");
            }
            return View(quizViewModel); 
        }

    }
}
