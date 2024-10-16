using System;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository;

public class CourseRepo : IRepo<Course>
{
  private readonly LearnFlowContext context;

  public CourseRepo(LearnFlowContext context)
  {
    this.context = context;
  }

  public async Task<IEnumerable<Course>> GetAllAsync()
  {
    return await context.Courses.ToListAsync();
  }

  public async Task<IEnumerable<Course>> GetAllForInstructorAsync(int instructorId)
  {
    return await context.Courses.Where(c => c.InstructorId == instructorId).ToListAsync();
  }

  public async Task<IEnumerable<Course>> GetAllForIsVerifiedAsync(){
    return await context.Courses.Where(c => c.IsVerified == true).ToListAsync();
  }
  public async Task<IEnumerable<Course>> GetAllForStudentAsync(int studentId)
  {
    return await context.Courses.Where(c => c.Enrollments.Any(e => e.StudentId == studentId)).ToListAsync();
  }

  public async Task<Course> GetByIdAsync(int id)
  {
    return await context.Courses
    .Include(c => c.Instructor)
    .Include(r => r.Reviews)
    .Include(l => l.Lectures)
    .Include(e => e.Enrollments.Count)
    .FirstOrDefaultAsync(c => c.CourseId == id);
  }

  public async Task<Course> CreateAsync(Course entity)
  {
    await context.Courses.AddAsync(entity);
    await context.SaveChangesAsync();
    return entity;
  }

  public async Task<Course> UpdateAsync(Course entity)
  {
    context.Courses.Update(entity);
    await context.SaveChangesAsync();
    return entity;
  }

  public async Task<Course> DeleteAsync(int id)
  {
    var entity = await context.Courses.FindAsync(id);
    if (entity == null)
    {
      return null;
    }

    context.Courses.Remove(entity);
    await context.SaveChangesAsync();
    return entity;
  }
  

}
