using System.Data;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
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

  public string ImageUrl { get; set; } = "default.png";

  public DateTime DateJoined { get; set; } = DateTime.Now;

  public required UserRole Role { get; set; }

  public ICollection<Course>? Courses { get; set; }

  public ICollection<Enrollment>? Enrollments { get; set; }

  public ICollection<QuizResult>? QuizResults { get; set; }

}