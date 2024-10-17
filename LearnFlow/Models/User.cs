using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace LearnFlow.Models;

public enum UserRole
{
  Student,
  Instructor
}

public class User : IdentityUser<int>
{
  [Required, DisplayName("Full Name")]
  public required string FullName { get; set; }

  public string ImageUrl { get; set; } = "https://res.cloudinary.com/dwwuefmsb/image/upload/v1729191590/default_d1m7rd.png";

  public DateTime DateJoined { get; set; } = DateTime.Now;

  public required UserRole Role { get; set; }

  public ICollection<Course>? Courses { get; set; }

  public ICollection<Enrollment>? Enrollments { get; set; }

  public ICollection<QuizResult>? QuizResults { get; set; }

}