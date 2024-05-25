using TechStore.Application.Models.Base;
using TechStore.Application.Models.Product;


namespace TechStore.Application.Models.Wishlist
{
    public class WishlistProductModel : BaseModel
    {
        public ProductReadModel Product { get; set; }
    }
}
