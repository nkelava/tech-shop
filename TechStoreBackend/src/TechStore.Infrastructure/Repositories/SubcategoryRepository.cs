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

        public async Task<Subcategory> GetSubcategoryByIdAsync(int subcategoryId)
        {
            return await FindByCondition(subcategory => subcategory.Id.Equals(subcategoryId)).Include(s => s.Category).FirstOrDefaultAsync();
        }

        public async Task<Subcategory> GetSubcategoryBySlugAsync(string subcategorySlug)
        {
            return await FindByCondition(subcategory => subcategory.Slug.Equals(subcategorySlug)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Subcategory>> GetAllSubcategoriesAsync()
        {
            return await FindAll().Include(s => s.Category).ToListAsync();
        }
    }
}
