

using System.ComponentModel.DataAnnotations;

namespace TechStore.Application.Models.Newsletter
{
    public class NewsletterCreateModel
    {
        [Required(ErrorMessage = "Please provide email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
