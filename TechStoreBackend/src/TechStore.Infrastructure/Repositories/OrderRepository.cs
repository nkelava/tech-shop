using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.OrderSpecification;
using TechStore.Domain.Entities.Order;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(TechStoreContext techStoreContext)
            : base(techStoreContext) { }

        public Order GetOrderById(int orderId)
        {
            var spec = new OrderWithProductsSpecification(orderId);
            var order = Find(spec).FirstOrDefault();

            return order;
        }

        public Order GetOrderByEmail(string email)
        {
            var spec = new OrderWithProductsSpecification(email);
            var order = Find(spec).FirstOrDefault();

            return order;
        }
        
        public IList<Order> GetAllOrders()
        {
            var orders = FindAll().ToList();

            return orders;
        }
    }
}
