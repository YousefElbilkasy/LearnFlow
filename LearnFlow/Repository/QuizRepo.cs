using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository
{
    public class QuizRepo : IRepo<Quiz>
    {
        private readonly LearnFlowContext context;

        public QuizRepo(LearnFlowContext context)
        {
            this.context = context;
        }

        public async Task<Quiz> CreateAsync(Quiz entity)
        {
            context.Quizs.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<Quiz> DeleteAsync(int id)
        {
            var quiz = await context.Quizs.FindAsync(id);
            if (quiz == null)
            {
                return null;
            }

            var quizResults = await context.QuizResults.Where(qr => qr.QuizId == id).ToListAsync();
            context.QuizResults.RemoveRange(quizResults);
            context.Quizs.Remove(quiz);
            await context.SaveChangesAsync();
            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await context.Quizs.Include(c => c.Course).ToListAsync();
        }

        public async Task<Quiz> GetByIdAsync(int id)
        {
            return await context.Quizs.FindAsync(id);
        }

        public async Task<Quiz> UpdateAsync(Quiz entity)
        {
            context.Quizs.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
