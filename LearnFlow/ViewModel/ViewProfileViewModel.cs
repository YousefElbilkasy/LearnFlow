using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.ViewModel;

public class ViewProfileViewModel
{
  public int Id { get; set; }

  [EmailAddress, Required]
  public required string Email { get; set; }

  [DisplayName("Name"), Required]
  public required string FullName { get; set; }

  [DisplayName("Profile Picture")]
  public string ImageUrl { get; set; } = "default.png";

  public required string UserName { get; set; }

  [Required, DisplayName("Phone Number")]
  public required string PhoneNumber { get; set; }

  [DisplayName("You Joined To Us From")]
  public DateTime DateJoined { get; set; }
}
