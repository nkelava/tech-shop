using TechStore.Application.Models.Order;


namespace TechStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderCreateModel orderModel);
        Task UpdateAsync(OrderUpdateModel orderModel);

        Task<int> UpdateOrderStatusAsync(OrderUpdateStatusModel updateModel);
        Task<int> DeleteAsync(int orderId);
        Task<OrderReadModel> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<OrderReadModel>> GetOrdersAsync();
        Task<IEnumerable<OrderReadModel>> GetOrdersAsync(string email);
    }
}
