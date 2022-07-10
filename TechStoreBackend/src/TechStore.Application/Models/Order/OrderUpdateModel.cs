using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Order
{
    public class OrderUpdateModel : BaseOrderModel
    {
        public int Id { get; set; }
        public DateTime ShippedAt { get; set; }
    }
}
