

namespace TechStore.Application.Models.Order
{
    public class DeliveryAddressModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ShippingAddress { get; set; }
        public int ZipCode { get; set; }
    }
}
