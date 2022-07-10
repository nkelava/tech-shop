using TechStore.Domain.Enums.Order;


namespace TechStore.Application.Models.Order
{
    public class BaseOrderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ShippingAddress { get; set; }
        public int PostalCode { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
