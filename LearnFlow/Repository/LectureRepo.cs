using System;
using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository;

public class LectureRepo : IRepo<Lecture>
{
  private readonly LearnFlowContext context;

  public LectureRepo(LearnFlowContext context)
  {
    this.context = context;
  }

  public async Task<Lecture> CreateAsync(Lecture entity)
  {
    await context.Lectures.AddAsync(entity);
    await context.SaveChangesAsync();
    return entity;
  }

  public async Task<Lecture> DeleteAsync(int id)
  {
    var lecture = await context.Lectures.FindAsync(id);
    if (lecture != null)
    {
      context.Lectures.Remove(lecture);
      await context.SaveChangesAsync();
    }
    return lecture;
  }

  public async Task<IEnumerable<Lecture>> GetAllAsync()
  {
    return await context.Lectures.ToListAsync();
  }

  public async Task<Lecture> GetByIdAsync(int id)
  {
    return await context.Lectures.FindAsync(id);
  }

  public IEnumerable<Course> SearchCourses(string searchKeyword)
  {
    throw new NotImplementedException();
  }

  public async Task<Lecture> UpdateAsync(Lecture entity)
  {
    context.Lectures.Update(entity);
    await context.SaveChangesAsync();
    return entity;
  }
}
