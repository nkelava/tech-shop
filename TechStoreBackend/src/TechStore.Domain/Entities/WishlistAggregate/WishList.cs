using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.Wishlist
{
    public class Wishlist : Entity
    {
        // n - n
        public IList<WishListProduct> Products { get; set; }
    }
}
