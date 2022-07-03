using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(TechStoreContext techStoreContext) : base(techStoreContext) { }

        public async Task<Brand> GetBrandByIdAsync(int brandId)
        {
            return await FindByCondition(brand => brand.Id.Equals(brandId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
