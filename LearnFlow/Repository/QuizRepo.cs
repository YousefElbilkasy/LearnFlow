using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var allQuizs = await _context.Quizs.ToListAsync();
            return allQuizs;
        }

        public async Task<Quiz> GetQuiz(int id)
        {
            var quiz = await _context.Quizs.Include(c => c.Course).FirstOrDefaultAsync(q => q.QuizId == id);
            return quiz;
        }
        public async Task<bool> AddQuiz()
        {
            _context.AddAsync()
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
