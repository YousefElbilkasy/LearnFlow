using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Controllers
{
    public class QuizController : Controller
    {
        private readonly IRepo<Quiz> _quizRepo;

        public QuizController(IRepo<Quiz> quizRepo)
        {
            _quizRepo = quizRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allQuizs = await _quizRepo.GetAllAsync();
            return View(allQuizs);
        }
        [HttpGet]
        public async Task<IActionResult> ViewQuiz(int quizId)
        {
            var quiz = await _quizRepo.GetByIdAsync(quizId);
            return View(quiz);
        }


        public async Task<IActionResult> Create()
        {
            var quizVM = new QuizViewModel() { Title = string.Empty };
            return View(quizVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuizViewModel quizViewModel)
        {
            if (ModelState.IsValid)
            {
                var Quiz = new Quiz
                {
                    Title = quizViewModel.Title,
                    MaxScore = quizViewModel.MaxScore,
                    CourseId = 6,
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

        public async Task<IActionResult> Delete(int id)
        {
            var delQuiz = await _quizRepo.GetByIdAsync(id);
            return View(delQuiz);
        }

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

    }
}
