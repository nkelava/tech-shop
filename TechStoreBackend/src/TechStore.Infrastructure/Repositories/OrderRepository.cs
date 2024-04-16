using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.OrderSpecification;
using TechStore.Domain.Entities.OrderAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(TechStoreContext techStoreContext)
            : base(techStoreContext) { }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var spec = new OrderWithProductsSpecification(orderId);
            return await Find(spec).FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(string email)
        {
            return await FindByCondition(o => o.Email.ToLower().Equals(email.ToLower())).ToListAsync();
        }
    }
}
