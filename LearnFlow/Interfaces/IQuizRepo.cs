using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Interfaces
{
  public interface IQuizRepo
  {
    public Task<IEnumerable<Quiz>> GetAllQuizes();
    public Task<Quiz> GetQuiz(string id);
    public Task<bool> SubmitAnswers(string id);
    public Task<QuizResult> ViewAnswers(string id);
  }
}
