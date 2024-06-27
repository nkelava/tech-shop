

using System.ComponentModel.DataAnnotations;

namespace TechStore.Application.Models.Order
{
    public class OrderCreateModel : BaseOrderModel
    {
        [Required(ErrorMessage = "Products list is required.")]
        [MinLength(1, ErrorMessage = "At least one product must be included in the order.")]
        public IList<OrderProductModel> Products { get; set; } = new List<OrderProductModel>();
        
        public DeliveryAddressModel? DeliveryAddress { get; set; }
    }
}
