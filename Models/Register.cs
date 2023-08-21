using System.ComponentModel.DataAnnotations;

namespace Test3.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 15 characters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email!")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+])[A-Za-z\d@#$%^&+]{8,}$",ErrorMessage ="Password must contain at least 8 characters")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
