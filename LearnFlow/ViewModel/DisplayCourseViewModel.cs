using System;
using LearnFlow.Models;

namespace LearnFlow.ViewModel;

public class DisplayCourseViewModel
{
  public int CourseId { get; set; }
  public string CourseTitle { get; set; }
  public string CourseDescription { get; set; }
  public User CourseInstructor { get; set; }
  public List<DisplayLectureViewModel> Lectures { get; set; }
  public DisplayLectureViewModel SelectedLecture { get; set; }
  public float Progress { get; set; }
}