using System;

namespace LearnFlow.ViewModel;

public class AddLecturesViewModel
{
  public int CourseId { get; set; }

  public List<LectureViewModel> Lectures { get; set; } = new List<LectureViewModel>();
}
