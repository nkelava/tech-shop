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
        
        public IList<Order> GetAllOrders()
        {
            var orders = FindAll().ToList();

            return orders;
        }

        public IList<Order> GetAllOrders(string email)
        {
            var orders = FindByCondition(o => o.Email.ToLower().Equals(email.ToLower())).ToList();

            return orders;
        }
    }
}
