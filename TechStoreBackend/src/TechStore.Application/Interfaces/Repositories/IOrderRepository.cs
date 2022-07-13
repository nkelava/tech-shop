using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.Order;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderById(int orderId);

        IList<Order> GetAllOrders();
        IList<Order> GetAllOrders(string email);
    }
}
