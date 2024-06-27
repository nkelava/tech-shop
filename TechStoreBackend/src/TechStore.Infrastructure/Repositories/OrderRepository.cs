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
        
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string email)
        {
            return await FindByCondition(o => o.Email.ToLower().Equals(email.ToLower())).ToListAsync();
        }
    }
}
