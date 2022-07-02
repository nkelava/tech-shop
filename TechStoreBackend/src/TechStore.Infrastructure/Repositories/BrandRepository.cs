using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(TechStoreContext techStoreContext) : base(techStoreContext) { }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await GetAllAsync();
        }
    }
}
