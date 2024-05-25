

namespace TechStore.Application.Models.Order
{
    public class OrderCreateModel : BaseOrderModel
    {
        public DeliveryAddressModel? DeliveryAddress { get; set; }
        public IList<OrderProductModel> Products { get; set; } = new List<OrderProductModel>();
    }
}
