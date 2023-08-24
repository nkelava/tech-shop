using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Domain.Entities.OrderAggregate
{
    public class OrderProduct
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }


        // n - n
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
