using TechStore.Domain.Entities.Base;
using TechStore.Domain.Enums.Order;


namespace TechStore.Domain.Entities.OrderAggregate
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
        public int ZipCode { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime ShippedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // 1 - 1
        public DeliveryAddress? DeliveryAddress { get; set; }

        // n - n
        public List<OrderProduct> Products { get; set; } = new List<OrderProduct> { };


        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (OrderProduct orderProduct in Products)
            {
                totalPrice = Decimal.Add(totalPrice, orderProduct.TotalPrice);
            }

            return totalPrice;
        }
    }
}
