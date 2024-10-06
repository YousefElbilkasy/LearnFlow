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

    public async Task<Quiz?> GetQuizById(int? id)
    {
      if (id == null)
      {
        return null;
      }

      var quiz = await _context.Quizs.FindAsync(id);
      return quiz;
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
