using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.ViewModel;

public class CourseViewModel
{
  // public int CourseId { get; set; }

  // public int InstructorId { get; set; } // ID of the instructor creating the course

  // [Required, DisplayName("Course Title")]
  // public required string Title { get; set; }

  // [Required, DisplayName("Course Description")]
  // public required string Description { get; set; }

  // [
  //   Required,
  //   DisplayName("Price"),
  //   Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")
  // ]
  // public decimal Price { get; set; }

  // [DisplayName("Course Image")]
  // public IFormFile? Image { get; set; } // File upload for the image

  // public string? ImageUrl { get; set; } // Path to the uploaded image (for display)

  // public DateTime CreationDate { get; set; } = DateTime.Now;

  // [Required]
  // public List<LectureViewModel> Lectures { get; set; }

  public int CourseId { get; set; }

  [Required(ErrorMessage = "Title is required.")]
  public string Title { get; set; }

  [Required(ErrorMessage = "Description is required.")]
  public string Description { get; set; }

  [Required(ErrorMessage = "Price is required.")]
  [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000.")]
  public decimal Price { get; set; }

  public IFormFile? Image { get; set; }

  // This will be populated automatically based on the logged-in instructor
  public int InstructorId { get; set; }
}
