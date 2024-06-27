using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class AttributeRepository : Repository<ProductAttribute>, IAttributeRepository
    {
        public AttributeRepository(TechStoreContext techStoreContext)
           : base(techStoreContext) { }


        public async Task<ProductAttribute?> GetByIdAsync(int id)
        {
            return await FindByCondition(a => a.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<ProductAttribute?> GetByNameAsync(string name)
        {
            return await FindByCondition(p => p.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductAttribute>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
