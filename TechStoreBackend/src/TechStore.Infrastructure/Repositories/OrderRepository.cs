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


        public async Task<Order?> GetByIdAsync(int id)
        {
            var spec = new OrderWithProductsSpecification(id);
            return await Find(spec).FirstOrDefaultAsync();
        }
        public async Task<Order?> GetBySessionIdAsync(string sessionId)
        {
            return await FindByCondition(o => o.SessionId != null && o.SessionId.Equals(sessionId))
                .Include(o => o.Products)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string userId)
        {
            return await FindByCondition(o => o.ApplicationUserId != null && o.ApplicationUserId.Equals(userId)).ToListAsync();
        }
    }
}
