using System;

namespace LearnFlow.ViewModel;

public class CourseDetailsViewModel
{
  public int CourseId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public decimal Price { get; set; }
  public string InstructorName { get; set; }
  public string ImageUrl { get; set; }

  public List<LectureViewModel> Lectures { get; set; } = new List<LectureViewModel>();
  public List<QuizViewModel> Quizzes { get; set; } = new List<QuizViewModel>();
}
