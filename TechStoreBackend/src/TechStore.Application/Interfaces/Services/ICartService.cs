using TechStore.Application.Models.Cart;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task AddProductAsync(string email, int quantity, int productId);
        Task RemoveProductAsync(int cartId, int productId);
        Task ClearCart(string email);

        Task<CartReadModel> GetByEmail(string email);
    }
}
