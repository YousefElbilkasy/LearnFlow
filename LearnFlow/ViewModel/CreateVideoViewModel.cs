using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnFlow.ViewModel
{
    public class CreateVideoViewModel
    {
        public int LectureId { get; set; }
        public int CourseId { get; set; }

        public string Title { get; set; }
        public IFormFile Content { get; set; }

        public int Order { get; set; }

    }
}
