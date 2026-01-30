using System.ComponentModel.DataAnnotations;

namespace AngularLoginApi.Model
{
    public class Register
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain uppercase, lowercase and number"
    )]
        public string Password { get; set; }
    }
}
