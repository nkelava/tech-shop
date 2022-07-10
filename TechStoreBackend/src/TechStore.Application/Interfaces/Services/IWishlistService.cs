
using TechStore.Application.Models.Wishlist;


namespace TechStore.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task AddProductAsync(string username, int productId);
        Task RemoveProductAsync(int wishlistId, int productId);

        Task<WishlistReadModel> GetByUsernameAsync(string username);
    }
}
