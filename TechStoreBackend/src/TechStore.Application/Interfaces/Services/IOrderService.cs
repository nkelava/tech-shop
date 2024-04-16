using TechStore.Application.Models.Order;


namespace TechStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderCreateModel orderModel);
        Task DeleteAsync(int orderId);
        Task UpdateAsync(OrderUpdateModel orderModel);

        Task<OrderReadModel> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<OrderReadModel>> GetOrdersAsync();
        Task<IEnumerable<OrderReadModel>> GetOrdersAsync(string email);
    }
}
