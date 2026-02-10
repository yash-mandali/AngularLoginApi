using System.ComponentModel.DataAnnotations;

namespace AngularLoginApi.Model
{
    public class ResetPassword
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string conformPassword { get; set; } 

    }
}
