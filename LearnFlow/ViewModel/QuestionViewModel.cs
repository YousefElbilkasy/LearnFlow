using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.ViewModel;

public class QuestionViewModel
{
  [Required, DisplayName("Question Text")]
  public required string Text { get; set; }

  public List<AnswerOptionViewModel> AnswerOptions { get; set; }
}
