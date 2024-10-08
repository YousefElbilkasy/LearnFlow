using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class QuizResult
{
  [Key]
  public int ResultId { get; set; }

  [Required]
  [ForeignKey("User")]
  public int StudentId { get; set; }

  [Required]
  [ForeignKey("Quiz")]
  public int QuizId { get; set; }

  public int Score { get; set; }

  public DateTime CompletionDate { get; set; } = DateTime.Now;

  // Navigation Properties
  public User? Student { get; set; }
  public Quiz? Quiz { get; set; }
}
