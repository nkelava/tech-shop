using TechStore.Application.Models.Wishlist;


namespace TechStore.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task<WishlistReadModel?> AddProductAsync(string username, int productId);
        Task<WishlistReadModel?> RemoveProductAsync(int wishlistId, int productId);
        Task<WishlistReadModel> GetByEmailAsync(string email);
    }
}
