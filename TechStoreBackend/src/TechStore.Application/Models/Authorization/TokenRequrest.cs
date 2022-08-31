
using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Authorization
{
    public class TokenRequrest
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
