using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class Question
{
  [Key]
  public int QuestionId { get; set; }

  [Required]
  [ForeignKey("Quiz")]
  public int QuizId { get; set; }

  [Required, DisplayName("Question")]
  public required string QuestionText { get; set; }

  // Navigation Property to related AnswerOptions
  public ICollection<AnswerOption> AnswerOptions { get; set; } // Collection of answer options

  // Navigation Property
  public Quiz Quiz { get; set; }
}
