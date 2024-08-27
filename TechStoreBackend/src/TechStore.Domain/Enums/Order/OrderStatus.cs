

namespace TechStore.Domain.Enums.Order
{
    public enum OrderStatus
    {
        Pending = 1,    // Order has been placed but not yet processed
        Confirmed = 2,  // Order has been confirmed (e.g., payment authorized for credit card, COD order acknowledged)
        Packing = 3,    // Order is being packed/prepared for shipment
        Shipped = 4,    // Order has been shipped out to the customer
        Delivered = 5,  // Order has been delivered to the customer
        Canceled = 6,   // Order was cancelled before shipping
        Returned = 7,   // Customer returned the order after delivery
        Refunded = 8    // Refund has been processed
    }
}
