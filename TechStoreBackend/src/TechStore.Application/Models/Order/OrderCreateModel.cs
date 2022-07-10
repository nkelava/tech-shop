

namespace TechStore.Application.Models.Order
{
    public class OrderCreateModel : BaseOrderModel
    {
        public IList<OrderProductModel> Products { get; set; } = new List<OrderProductModel>();
    }
}
