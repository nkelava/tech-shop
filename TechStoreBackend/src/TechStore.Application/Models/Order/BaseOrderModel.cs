using TechStore.Domain.Enums.Order;

namespace TechStore.Application.Models.Order
{
    public class BaseOrderModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ShippingAddress { get; set; }
        public int ZipCode { get; set; }
        public string? ApplicationUserId { get; set; } = null;
        public PaymentType? PaymentMethod { get; set; } = PaymentType.Cash;
        public OrderStatus? Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus? PaymentStatus { get; set; } = null;
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
