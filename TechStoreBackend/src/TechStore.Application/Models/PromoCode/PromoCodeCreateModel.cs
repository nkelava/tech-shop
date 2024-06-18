using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.PromoCode
{
    public class PromoCodeCreateModel
    {
        [Required(ErrorMessage = "Promo code is required.")]
        public string Code { get; set; }
        
        [Required(ErrorMessage = "Discount is required.")]
        [Range(1, 100, ErrorMessage = "Discount must be between 1 and 100.")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Expiration date is required.")]
        public DateTime ExpirationDate { get; set; }
    }
}
