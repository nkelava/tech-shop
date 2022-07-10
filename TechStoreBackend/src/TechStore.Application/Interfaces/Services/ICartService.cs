using TechStore.Application.Models.Cart;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task AddProductAsync(string username, int movieId);
        Task RemoveProductAsync(int cartId, int productId);
        Task ClearCart(string username);

        Task<CartReadModel> GetByUsername(string username);
    }
}
