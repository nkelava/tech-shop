using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Domain.Entities.Cart
{
    public class CartProduct : Entity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        

        // n - n
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
