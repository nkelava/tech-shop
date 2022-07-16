using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(TechStoreContext techStoreContext)
           : base(techStoreContext) { }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await FindByCondition(p => p.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Property> GetPropertyByNameAsync(string name)
        {
            return await FindByCondition(p => p.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}
