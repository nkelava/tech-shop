using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Cart
{
    public class CartCreateModel
    {
        [Required(ErrorMessage = "Please provide a product ID.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please provide a product quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1.")]
        public int Quantity { get; set; }
    }
}
