using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LearnFlow.Models;

namespace LearnFlow.ViewModel;

public enum Role
{
  Student,
  Instructor
}

public class RegisterUserViewModel
{
  [DisplayName("Full Name")]
  public required string FullName { get; set; }

  [DisplayName("User Name")]
  public required string UserName { get; set; }

  public required string Email { get; set; }

  [DataType(DataType.Password)]
  public required string Password { get; set; }

  [
    DisplayName("Confirm Password"),
    Compare("Password"),
    DataType(DataType.Password)
  ]
  public required string ConfirmPassword { get; set; }

  [DisplayName("Phone Number"),
  DataType(DataType.PhoneNumber),
  MaxLength(11)]
  public required string PhoneNumber { get; set; }

  [DisplayName("Profile Picture")]
  public IFormFile? ImageUrl { get; set; }

  [Required]
  public required UserRole Role { get; set; }
}
