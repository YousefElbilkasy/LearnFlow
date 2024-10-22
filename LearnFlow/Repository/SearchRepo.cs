using System.Collections.Generic;
using System.Threading.Tasks;
using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository
{
    public class SearchRepo :ISearchRepo
    {
        private readonly LearnFlowContext _context;

        public SearchRepo(LearnFlowContext context)
        {
            _context = context;
        }
        public IEnumerable<Course> SearchCourses(string searchKeyword)
        {
            return _context.Courses
                .Where(c => c.Title.Contains(searchKeyword) || c.Description.Contains(searchKeyword))
                .ToList();
        }
    }
}







