using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Models;

public class Course
{
  [Key]
  public int CourseId { get; set; }

  [ForeignKey("Instructor")]
  public int InstructorId { get; set; }
  [Required]
  public required string Title { get; set; }
  [Required]
  public required string Description { get; set; }
  [Precision(18, 2)]
  public decimal Price { get; set; }
  [DisplayName("Creation Date")]
  public DateTime CreationDate { get; set; } = DateTime.Now;
  public string ImageUrl { get; set; } = "https://res.cloudinary.com/dwwuefmsb/image/upload/v1729191287/utaberriwu5sbav4h7ar.png";

  public bool IsVerified { get; set; } = false;

  // Navigation Properties
  public User? Instructor { get; set; }
  public ICollection<Lecture>? Lectures { get; set; }
  public ICollection<Enrollment>? Enrollments { get; set; }
  public ICollection<Quiz>? Quizzes { get; set; }
  public ICollection<Review>? Reviews { get; set; }
  public ICollection<Payment>? Payments { get; set; }

}
