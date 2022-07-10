using TechStore.Application.Models.Order;


namespace TechStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task AddAsync(OrderCreateModel orderModel);
        Task DeleteAsync(int orderId);
        Task UpdateAsync(OrderUpdateModel orderModel);

        OrderReadModel GetOrderById(int orderId);
        OrderReadModel GetOrderByEmail(string email);

        IList<OrderReadModel> GetOrders();
    }
}
