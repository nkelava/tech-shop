using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.OrderAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> GetBySessionIdAsync(string sessionId);

        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetAllAsync(string userId);
    }
}
