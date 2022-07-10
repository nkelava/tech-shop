using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.Order;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderById(int orderId);
        Order GetOrderByEmail(string email);

        IList<Order> GetAllOrders();
    }
}
