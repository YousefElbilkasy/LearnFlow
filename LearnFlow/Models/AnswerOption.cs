using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class AnswerOption
{
  [Key]
  public int AnswerOptionId { get; set; }

  [Required]
  public required string OptionText { get; set; } // The text for the answer option

  [Required]
  public bool IsCorrect { get; set; } // Indicates if this option is the correct answer

  [ForeignKey("Question")]
  public int QuestionId { get; set; } // Foreign key linking to the Question

  // Navigation Property
  public Question Question { get; set; } // The question this option belongs to
}
