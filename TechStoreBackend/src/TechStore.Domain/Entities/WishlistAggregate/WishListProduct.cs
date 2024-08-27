using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Domain.Entities.WishlistAggregate
{
    public class WishlistProduct
    {
        // n - n
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
