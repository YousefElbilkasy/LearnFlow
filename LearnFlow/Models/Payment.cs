using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Models;

public class Payment
{
  [Key]
  public int PaymentId { get; set; }

  [Required]
  [ForeignKey("User")]
  public int StudentId { get; set; }

  [Required]
  [ForeignKey("Course")]
  public int CourseId { get; set; }

  [Required, Precision(18, 2) ]
  public decimal Amount { get; set; }
  [DisplayName("Payment Date")]
  public DateTime PaymentDate { get; set; } = DateTime.Now;

  // Navigation Properties
  public User? Student { get; set; }
  public Course? Course { get; set; }
}
