using System;

namespace LearnFlow.ViewModel;

public class AddQuestionsViewModel
{
  public int QuizId { get; set; }

  public int CourseId { get; set; }

  public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
}
