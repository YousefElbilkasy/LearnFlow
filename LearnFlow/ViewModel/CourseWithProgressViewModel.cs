using System;

namespace LearnFlow.ViewModel
{
    public class CourseWithProgressViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public float Progress { get; set; } // Add progress property
    }
}
