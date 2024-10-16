using System;
using System.ComponentModel;
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

  [Required]
  public required string Text { get; set; }

  // Navigation Property to related AnswerOptions
  public ICollection<Answer>? Answers { get; set; } // Collection of answer options

  // Navigation Property
  public Quiz? Quiz { get; set; }
}
