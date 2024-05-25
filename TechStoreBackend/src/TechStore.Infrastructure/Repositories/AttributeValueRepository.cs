using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class AttributeValueRepository : Repository<ProductAttributeValue>, IAttributeValueRepository
    {
        public AttributeValueRepository(TechStoreContext techStoreContext)
           : base(techStoreContext) { }

        public async Task<ProductAttributeValue> GetByIdAsync(int id)
        {
            return await FindByCondition(av => av.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductAttributeValue>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
