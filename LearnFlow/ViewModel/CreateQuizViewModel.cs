using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.ViewModel;

public class CreateQuizViewModel
{
  public int CourseId { get; set; }

  [Required, DisplayName("Quiz Title")]
  public string Title { get; set; } = string.Empty;

  [Required, DisplayName("Maximum Score")]
  public double MaxScore { get; set; }

}
