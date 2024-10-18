using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class Enrollment
{
  [Key]
  public int EnrollmentId { get; set; }
  [Required]
  [ForeignKey("Student")]
  public int StudentId { get; set; }
  [ForeignKey("Course")]
  [Required]
  public int CourseId { get; set; }
  [DisplayName("Enrollment Date")]
  public DateTime EnrollmentDate { get; set; } = DateTime.Now;
  public float Progress { get; set; }
  public Course? Course { get; set; }
  public User? Student { get; set; }
}
