using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.CartSpecification;
using TechStore.Domain.Entities.CartAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(TechStoreContext techStoreContext)
            : base(techStoreContext) { }


        public async Task<Cart?> GetByUserIdAsync(string id)
        {
            var spec = new CartWithProductsSpecification(id);
            return await Find(spec).FirstOrDefaultAsync();
        }
    }
}
