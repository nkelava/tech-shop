
using TechStore.Application.Models.Wishlist;


namespace TechStore.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task AddProductAsync(string username, int productId);
        Task<int> RemoveProductAsync(int wishlistId, int productId);

        Task<WishlistReadModel> GetByEmailAsync(string email);
    }
}
