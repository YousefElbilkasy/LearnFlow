using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace LearnFlow.ViewModel;

public class EditProfileViewModel
{
  public int Id { get; set; }

  [EmailAddress, Required]
  public required string Email { get; set; }

  [Required, DisplayName("Full Name")]
  public required string FullName { get; set; }

  [DisplayName("New Image")]
  public IFormFile? NewImageUrl { get; set; }

  public string? ImageUrl { get; set; }

  [Required, DisplayName("User Name")]
  public required string UserName { get; set; }

  [Required, DisplayName("Phone Number"), DataType(DataType.PhoneNumber)]
  public required string PhoneNumber { get; set; }
}
