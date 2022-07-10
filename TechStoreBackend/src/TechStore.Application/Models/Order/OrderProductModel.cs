using TechStore.Application.Models.Product;


namespace TechStore.Application.Models.Order
{
    public class OrderProductModel
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }
    }
}
