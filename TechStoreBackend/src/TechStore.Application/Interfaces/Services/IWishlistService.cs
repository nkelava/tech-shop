using TechStore.Application.Models.Wishlist;
using TechStore.Domain.Entities.User;


namespace TechStore.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task<WishlistReadModel?> AddProductAsync(ApplicationUser user, int productId);
        Task<WishlistReadModel?> RemoveProductAsync(int wishlistId, int productId);
        Task<WishlistReadModel> GetAsync(ApplicationUser user);
    }
}
