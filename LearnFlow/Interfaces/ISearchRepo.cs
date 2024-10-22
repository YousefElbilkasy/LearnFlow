using LearnFlow.Models;

namespace LearnFlow.Interfaces
{
    public interface ISearchRepo
    {
        IEnumerable<Course> SearchCourses(string searchKeyword);
    }
}
