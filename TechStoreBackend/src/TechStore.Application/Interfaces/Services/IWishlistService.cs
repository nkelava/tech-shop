
using TechStore.Application.Models.Wishlist;


namespace TechStore.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task AddItem(string username, int productId);
        Task RemoveItem(int wishlistId, int productId);

        Task<WishlistReadModel> GetWishlistByUsernameAsync(string username);
    }
}
