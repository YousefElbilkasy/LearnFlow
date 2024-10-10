using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LearnFlow.Models
{
    public class ApplicationUser
    {
        [Key]
        public int UserId { get; set; }  // PK
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }  // Email
        [Required(ErrorMessage = "password is required")]

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must be at least {2} and at most {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%?&]{8,}$",
        ErrorMessage = "Passwords must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string PasswordHash { get; set; }  // PasswordHash
        public int Role { get; set; }  // Role (Admin, Instructor, Student)
        [Required(ErrorMessage = "Full Name is required")]

        public string FullName { get; set; }  // Full Name
        public DateTime DateJoined { get; set; } = DateTime.Now;  // Date Joined
     
    }
}
