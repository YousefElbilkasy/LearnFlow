using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.ViewModel;

public class QuizViewModel
{
    [Required]
    public List<QuestionViewModel>? Questions { get; set; }

    [Required, MinLength(3)]
    public required string Title { get; set; }

    [Required]
    public int CourseId { get; set; }   

    [Required, DisplayName("Max Score")]
    public int MaxScore { get; set; }

    // Navigation Property to related AnswerOptions
}
