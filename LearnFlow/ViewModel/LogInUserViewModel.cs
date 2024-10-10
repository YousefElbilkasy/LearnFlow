using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.ViewModel;

public class LogInUserViewModel
{
  [Required, EmailAddress]
  public required string Email { get; set; }

  [Required, DataType(DataType.Password)]
  public required string Password { get; set; }

  [DisplayName("Remember Me")]
  public bool RememberMe { get; set; }
}
