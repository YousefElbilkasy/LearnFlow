using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.ViewModel;

public class AnswerOptionViewModel
{
  [Required, DisplayName("Option Text")]
  public required string OptionText { get; set; }

  [DisplayName("Is Correct")]
  public bool IsCorrect { get; set; }
}
