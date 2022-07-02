using TechStore.Domain.Entities.Base;
using TechStore.Domain.Enums.Order;


namespace TechStore.Domain.Entities.Order
{
    public class Order : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ShippingAddress { get; set; }
        public int PostalCode { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime ShippedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        // n - n
        public IList<OrderProduct> Products { get; set; }
    }
}
