using System.ComponentModel.DataAnnotations;

namespace AngularLoginApi.Model
{
    public class ContactEmailservice
    {
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
