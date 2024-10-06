using System.Data;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace LearnFlow.Models;

public enum UserRole
{
  Student,
  Instructor,
  Admin
}

public class User
{
  [Key]
  public int UserId { get; set;}
  [Required, EmailAddress,]
  public required string Email { get; set;}
  [Required]
  public required string PasswordHash { get; set;}
  public UserRole Role { get; set; }
  [Required, DisplayName("Full Name")]
  public required string FullName { get; set; }
  [Required]
  public required string ImageUrl { get; set; }
  public DateTime DateJoined { get; set; } = DateTime.Now;
  public ICollection<Course> Courses { get; set; }
  public ICollection<Enrollment> Enrollments { get; set; }
  public ICollection<QuizResult> QuizResults { get; set; }
}