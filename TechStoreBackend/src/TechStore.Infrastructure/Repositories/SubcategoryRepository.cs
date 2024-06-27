using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class SubcategoryRepository : Repository<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(TechStoreContext techStoreContext) 
            : base(techStoreContext) { }


        public async Task<Subcategory?> GetByIdAsync(int id)
        {
            return await FindByCondition(s => s.Id.Equals(id)).Include(s => s.Category).FirstOrDefaultAsync();
        }

        public async Task<Subcategory?> GetBySlugAsync(string id)
        {
            return await FindByCondition(s => s.Slug.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Subcategory?>> GetAllAsync()
        {
            return await FindAll().Include(s => s.Category).ToListAsync();
        }
    }
}
