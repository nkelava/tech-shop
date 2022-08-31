using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Authorization
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
