using System;
using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository;

public class QuestionRepo : IRepo<Question>
{
  private readonly LearnFlowContext context;
  public QuestionRepo(LearnFlowContext context)
  {
    this.context = context;
  }
  public async Task<Question> CreateAsync(Question entity)
  {
    context.Questions.Add(entity);
    await context.SaveChangesAsync();
    return entity;
  }

  public async Task<Question> DeleteAsync(int id)
  {
    var question = await context.Questions.FindAsync(id);
    if (question == null)
    {
      return null;
    }
    context.Questions.Remove(question);
    await context.SaveChangesAsync();
    return question;
  }

  public async Task<IEnumerable<Question>> GetAllAsync()
  {
    return await context.Questions.ToListAsync();
  }

  public async Task<Question> GetByIdAsync(int id)
  {
    return await context.Questions.FindAsync(id);
  }

  public IEnumerable<Course> SearchCourses(string searchKeyword)
  {
    throw new NotImplementedException();
  }

  public async Task<Question> UpdateAsync(Question entity)
  {
    context.Questions.Update(entity);
    await context.SaveChangesAsync();
    return entity;
  }
}
