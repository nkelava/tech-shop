

using TechStore.Domain.Enums.Order;

namespace TechStore.Application.Models.Order
{
    public class OrderReadModel : BaseOrderModel
    {
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DeliveryAddressModel DeliveryAddress { get; set; }

        public IList<OrderProductModel> Products { get; set; } = new List<OrderProductModel>();
    }
}
