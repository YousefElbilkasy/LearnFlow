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

            var quizQuestions = await context.Questions.Where(question => question.QuizId == id).ToListAsync();
            var questionsIds = quizQuestions.Select(q => q.QuestionId).ToList();
            var answers = await context.Answers.Where(answer => questionsIds.Contains(answer.QuestionId)).ToListAsync();
            context.Answers.RemoveRange(answers);
            context.Questions.RemoveRange(quizQuestions);


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
            return await context.Quizs.Include(c => c.Course).FirstOrDefaultAsync(q => q.QuizId == id);
        }

    public IEnumerable<Course> SearchCourses(string searchKeyword)
    {
      throw new NotImplementedException();
    }

    public async Task<Quiz> UpdateAsync(Quiz entity)
        {
            context.Quizs.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
