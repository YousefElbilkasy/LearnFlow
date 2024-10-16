using System;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository;
public class EnrollmentRepo : IEnrollmentRepo
{
    private readonly LearnFlowContext context;

    public EnrollmentRepo(LearnFlowContext context)
    {
        this.context = context;
    }

    public async Task<Enrollment?> GetEnrollmentByStudentAndCourseAsync(int studentId, int courseId)
    {
        return await context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
    }

    public async Task EnrollStudentAsync(Enrollment enrollment)
    {
        await context.Enrollments.AddAsync(enrollment);
        await context.SaveChangesAsync();
    }
}
