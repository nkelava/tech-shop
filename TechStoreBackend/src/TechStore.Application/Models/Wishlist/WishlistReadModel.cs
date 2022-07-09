using TechStore.Application.Models.Base;
using TechStore.Application.Models.Product;


namespace TechStore.Application.Models.Wishlist
{
    public class WishlistReadModel : ModelBase
    {
        public string Username { get; set; }
        public List<ProductReadModel> WishlistProducts { get; set; } = new List<ProductReadModel>();
    }
}
