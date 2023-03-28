using System.ComponentModel.DataAnnotations;

namespace Users.Models
{
    public class Login
    {
        //check if prop names need to be changed
        [Required(ErrorMessage = "User Name is required")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
