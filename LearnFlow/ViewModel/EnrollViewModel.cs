using System;
using System.ComponentModel.DataAnnotations;
using LearnFlow.Models;

namespace LearnFlow.ViewModel;

public class EnrollViewModel
{
  public CourseEnrollViewModel Course { get; set; }

  [Required(ErrorMessage = "Full Name is required")]
  [Display(Name = "Full Name")]
  public string FullName { get; set; }

  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid Email Address")]
  public string Email { get; set; }

  [Required(ErrorMessage = "Phone Number is required")]
  [Phone(ErrorMessage = "Invalid Phone Number")]
  [Display(Name = "Phone Number")]
  public string PhoneNumber { get; set; }
}


public class CourseEnrollViewModel
{
  public string Title { get; set; }
  public string Description { get; set; }
  public string ImageUrl { get; set; }
  public int CourseId { get; set; }
  public int InstructorId { get; set; }
  public decimal Price { get; set; }
  public DateTime CreationDate { get; set; }
}