using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.OrderAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetAllOrdersAsync(string email);
    }
}
