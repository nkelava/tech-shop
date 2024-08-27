using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.User;
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
        public PaymentType PaymentMethod { get; set; } = PaymentType.Cash;
        public PaymentStatus? PaymentStatus { get; set; } = null;
        public string? SessionId { get; set; } = null;
        public string? PaymentIntentId { get; set; } = null;
        public DateTime? ShippedAt { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // 1 - 1
        public DeliveryAddress? DeliveryAddress { get; set; }

        // n - 1
        public string? ApplicationUserId { get; set; } = null;
        public virtual ApplicationUser ApplicationUser { get; set; }

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

        public void UpdateOrderStatus<T>(int statusValue)
        {
            var isValid = ValidateStatusValue<OrderStatus>(statusValue);
            if (!isValid) return;
            this.Status = (OrderStatus)statusValue;
        }

        public void UpdateOrderPaymentStatus(int statusValue)
        {
            var isValid = ValidateStatusValue<PaymentStatus>(statusValue);
            if (!isValid) return;
            this.PaymentStatus = (PaymentStatus)statusValue;
        }

        public static bool ValidateStatusValue<T>(int statusValue) {
            return Enum.IsDefined(typeof(T), statusValue);
        }
    }
}
