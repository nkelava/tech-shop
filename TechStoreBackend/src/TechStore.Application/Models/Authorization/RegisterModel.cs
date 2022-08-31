using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Authorization
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
