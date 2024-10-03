using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Interfaces
{
  public interface IQuizRepo
  {
    public Task<IActionResult> GetAllQuizes();
    public Task<IActionResult> GetQuiz(string id);
    public Task<IActionResult> SubmitAnswers(string id);
    public Task<IActionResult> ViewAnswers(string id);
  }
}
