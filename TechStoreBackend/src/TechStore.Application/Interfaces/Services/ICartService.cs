using TechStore.Application.Models.Cart;
using TechStore.Domain.Entities.User;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartReadModel?> AddProductAsync(ApplicationUser user, CartCreateModel createModel);
        Task<CartReadModel?> RemoveProductAsync(int cartId, int productId);
        Task<CartReadModel?> ClearCart(ApplicationUser user);
        Task<CartReadModel> GetAsync(ApplicationUser user);
    }
}
