using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Domain.Entities.Wishlist
{
    public class WishListProduct : Entity
    {
        // n - n
        public int WishListId { get; set; }
        public Wishlist WishList { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
