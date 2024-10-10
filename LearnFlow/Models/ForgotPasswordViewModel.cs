using System.ComponentModel.DataAnnotations;

namespace LearnFlow.Models
{
    public class ForgotPasswordViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
