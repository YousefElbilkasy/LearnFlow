using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

public class CourseRepo : IRepo<Course>
{
    private readonly LearnFlowContext context;

    public CourseRepo(LearnFlowContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        // Ensure Instructor is included when fetching all courses
        return await context.Courses
            .Include(c => c.Instructor)
            .ToListAsync();
    }

    public async Task<Course> GetByIdAsync(int id)
    {
        // Fetch the course along with necessary related data
        return await context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Reviews)
            .Include(c => c.Lectures)
            .Include(c => c.Enrollments) // Just include enrollments, count later if needed
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