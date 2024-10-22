namespace LearnFlow.ViewModel
{
    public class EditVideoViewModel
    {
        public int LectureId { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public int Order{ get; set; }
        public string CurrentContentUrl { get; set; }
        public IFormFile? NewContent { get; set; }
    }

}