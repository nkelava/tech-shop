using TechStore.Domain.Entities.Base;

namespace TechStore.Domain.Entities.OrderAggregate
{
    public class DeliveryAddress : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ShippingAddress { get; set; }
        public int ZipCode { get; set; }

        // 1 - 1
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
