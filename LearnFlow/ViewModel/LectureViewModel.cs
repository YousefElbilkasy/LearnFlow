using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.ViewModel;

public class LectureViewModel
{
  [Required, DisplayName("Lecture Title")]
  public required string Title { get; set; }

  public string VideoUrl { get; set; }

  [Required, DisplayName("Video File")]
  public IFormFile Video { get; set; } // Optional video file

  [DisplayName("Extra Content (Optional)")]
  public IFormFile? Content { get; set; } // Optional content file like PDF, docs

  public string? ContentUrl { get; set; } // Path to the uploaded content (for display)

  public int Order { get; set; }

  [DisplayName("Quiz (Optional)")]
  public QuizViewModel Quiz { get; set; }
}