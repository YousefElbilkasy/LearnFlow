using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class Lecture
{
  [Key]
  public int LectureId { get; set; }

  [ForeignKey("Course")]
  public int CourseId { get; set; }

  [Required]
  public required string Title { get; set; }

  [Required]
  public required string Content { get; set; }

  public int Order { get; set; }

  public Course? Course { get; set; }
    
}
