using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Interfaces
{
    public interface IQuizRepo
    {
        public Task<IEnumerable<Quiz>> GetAllQuizes();
        public Task<Quiz> GetQuiz(int id);
        public Task<bool> AddQuiz(Quiz quiz);
        public Task<IActionResult> SubmitAnswers(string id);
        public Task<IActionResult> ViewAnswers(string id);
    }
}
