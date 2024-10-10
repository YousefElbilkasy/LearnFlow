using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class Quiz
{
  public int QuizId { get; set; }
  [ForeignKey("Course"), Required]
  public int CourseId { get; set; }
  [Required]
  public required string Title { get; set; }
  public double MaxScore { get; set; }
  public Course? Course { get; set; }
  public ICollection<Question>? Questions { get; set; }
}
