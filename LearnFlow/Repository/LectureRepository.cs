using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly LearnFlowContext _context;

        public LectureRepository(LearnFlowContext context)
        {
            _context = context;
        }
public async Task<IEnumerable<Lecture>> GetLecturesByCourseIdAsync(int courseId)
{
    return await _context.Lectures.Where(l => l.CourseId == courseId).ToListAsync();
}

        public async Task<IEnumerable<Lecture>> GetAllLecturesAsync()
        {
            return await _context.Lectures.ToListAsync();
        }

        public async Task<Lecture> GetLectureByIdAsync(int id)
        {
            return await _context.Lectures.FirstOrDefaultAsync(l => l.LectureId == id);
        }

        public async Task AddLectureAsync(Lecture lecture)
        {
            await _context.Lectures.AddAsync(lecture);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLectureAsync(int id)
        {
            var lecture = await GetLectureByIdAsync(id);
            if (lecture != null)
            {
                _context.Lectures.Remove(lecture);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateLectureAsync(Lecture lectureToUpdate)
        {
            var existingLecture = await _context.Lectures.FindAsync(lectureToUpdate.LectureId);

            if (existingLecture != null)
            {
                _context.Entry(existingLecture).CurrentValues.SetValues(lectureToUpdate);
                _context.Entry(existingLecture).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

    }

}
