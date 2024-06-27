using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.PromoCode
{
    public class PromoCodeCreateModel
    {
        [Required(ErrorMessage = "Please provide a promo code.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please specify a discount.")]
        [Range(1, 100, ErrorMessage = "Discount must be between 1% and 100%.")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Please provide an expiration date.")]
        public DateTime ExpirationDate { get; set; }
    }
}
