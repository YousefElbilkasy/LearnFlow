using LearnFlow.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Repository
{
  public class QuizRepo : IQuizRepo
  {
    public Task<IActionResult> GetAllQuizes()
    {
      throw new NotImplementedException();
    }

    public Task<IActionResult> GetQuiz(string id)
    {
      throw new NotImplementedException();
    }

    public Task<IActionResult> SubmitAnswers(string id)
    {
      throw new NotImplementedException();
    }

    public Task<IActionResult> ViewAnswers(string id)
    {
      throw new NotImplementedException();
    }
  }
}
