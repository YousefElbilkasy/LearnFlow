using System.Collections.Generic;
using System.Threading.Tasks;
using LearnFlow.Models;

namespace LearnFlow.Interfaces
{
    public interface IStudentRepo
    {
        Task<User> GetStudentById(int id);
        Task<List<Enrollment>> GetEnrollmentsByStudentId(int studentId);
        Task<List<QuizResult>> GetQuizResultsByStudentId(int studentId);
    }
}
