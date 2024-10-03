using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LearnFlow.Repository
{
  public class QuizRepo : IQuizRepo
  {
    private readonly LearnFlowContext _context;

    public QuizRepo(LearnFlowContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Quiz>> GetAllQuizes()
    {
      var quizes = await _context.Quizs.Include(x=>x.Course).ToListAsync();
      return quizes;
    }

    public Task<Quiz> GetQuiz(string id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> SubmitAnswers(string id)
    {
      throw new NotImplementedException();
    }

    public Task<QuizResult> ViewAnswers(string id)
    {
      throw new NotImplementedException();
    }
  }
}
