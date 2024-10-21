using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnFlow.Models;

public class Progress
{
  [Key, Column(Order = 0)]
  [ForeignKey("Enrollment")]
  public int EnrollmentId { get; set; }

  [Key, Column(Order = 1)]
  [ForeignKey("Lecture")]
  public int LectureId { get; set; }

  public bool IsCompleted { get; set; } = false;

  public Enrollment Enrollment { get; set; }

  public Lecture Lecture { get; set; }
}
