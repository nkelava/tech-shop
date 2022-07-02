using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.Cart
{
    public class Cart : Entity
    {
        public decimal TotalPrice { get; set; } = 0;


        // n - n
        public IList<CartProduct> Products { get; set; }
    }
}
