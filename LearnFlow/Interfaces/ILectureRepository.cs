using System.Collections.Generic;
using System.Threading.Tasks;
using LearnFlow.Models;

namespace LearnFlow.Interfaces
{
    public interface ILectureRepository
    {
        Task<IEnumerable<Lecture>> GetAllLecturesAsync();
        Task<Lecture> GetLectureByIdAsync(int id);
        Task AddLectureAsync(Lecture lecture);
        Task DeleteLectureAsync(int id);

        Task UpdateLectureAsync(Lecture lectureToUpdate);
        Task<IEnumerable<Lecture>> GetLecturesByCourseIdAsync(int courseId);

    }
}
