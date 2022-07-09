using TechStore.Application.Models.Cart;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task AddProduct(string username, int movieId);
        Task RemoveProduct(int cartId, int productId);
        Task ClearCart(string username);

        Task<CartReadModel> GetByUsername(string username);
    }
}
