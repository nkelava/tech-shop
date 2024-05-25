using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Wishlist
{
    public class WishlistReadModel : BaseModel
    {
        public List<WishlistProductModel> Products { get; set; } = new List<WishlistProductModel>();
    }
}
