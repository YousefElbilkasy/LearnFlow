using System.Collections.Generic;
using System.Threading.Tasks;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly LearnFlowContext _context;

        public StudentRepo(LearnFlowContext context)
        {
            _context = context;
        }

        public async Task<User> GetStudentById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Enrollment>> GetEnrollmentsByStudentId(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course) // نضمّن بيانات الكورس مع الـ Enrollment
                .ToListAsync();
        }

        public async Task<List<QuizResult>> GetQuizResultsByStudentId(int studentId)
        {
            return await _context.QuizResults
                .Include(q => q.Quiz) // Include related Quiz data
                .Where(q => q.StudentId == studentId)
                .ToListAsync();
        }
    }
}