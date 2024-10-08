using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class Review
{
  [Key]
  public int ReviewId { get; set; }

  [Required]
  [ForeignKey("Course")]
  public int CourseId { get; set; }

  [Required]
  [ForeignKey("User")]
  public int StudentId { get; set; }

  [Range(1, 5)]
  public int Rating { get; set; }

  public string? ReviewText { get; set; }

  // Navigation Properties
  public Course? Course { get; set; }
  public User? Student { get; set; }
}
