using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LearnFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.ViewModel;

public class CourseViewModel
{
  public int InstructorId { get; set; } // ID of the instructor creating the course

  [Required, DisplayName("Course Title")]
  public required string Title { get; set; }

  [Required, DisplayName("Course Description")]
  public required string Description { get; set; }

  [
    Required,
    DisplayName("Price"),
    Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")
  ]
  public decimal Price { get; set; }

  [DisplayName("Course Image")]
  public IFormFile? Image { get; set; } // File upload for the image

  public string? ImageUrl { get; set; } // Path to the uploaded image (for display)

  public DateTime CreationDate { get; set; } = DateTime.Now;

  [Required]
  public List<LectureViewModel> Lectures { get; set; }
}
