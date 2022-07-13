using TechStore.Application.Models.Order;


namespace TechStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task AddAsync(OrderCreateModel orderModel);
        Task DeleteAsync(int orderId);
        Task UpdateAsync(OrderUpdateModel orderModel);

        OrderReadModel GetOrderById(int orderId);

        IList<OrderReadModel> GetOrders();
        IList<OrderReadModel> GetOrders(string email);
    }
}
