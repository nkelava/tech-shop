using TechStore.Application.Models.Cart;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task AddProductAsync(string email, int productId, int quantity);
        Task RemoveProductAsync(int cartId, int productId);
        Task<int> ClearCart(string email);

        Task<CartReadModel> GetByEmailAsync(string email);
    }
}
