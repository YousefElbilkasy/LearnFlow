
    using System.ComponentModel.DataAnnotations;

    namespace LearnFlow.ViewModels
    {
        public class CourseViewModel
        {
            public int CourseId { get; set; }

            [Required(ErrorMessage = "Title is required.")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Description is required.")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Price is required.")]
            [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000.")]
            public decimal Price { get; set; }

            // This will be populated automatically based on the logged-in instructor
            public int InstructorId { get; set; }
        }
    }

