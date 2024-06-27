using TechStore.Application.Models.Cart;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartReadModel?> AddProductAsync(string email, CartCreateModel createModel);
        Task<CartReadModel?> RemoveProductAsync(int cartId, int productId);
        Task<CartReadModel?> ClearCart(string email);
        Task<CartReadModel> GetByEmailAsync(string email);
    }
}
