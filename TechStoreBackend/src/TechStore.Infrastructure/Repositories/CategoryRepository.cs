using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.CategorySpecification;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(TechStoreContext techStoreContext) 
            : base(techStoreContext) { }


        public async Task<Category?> GetByIdAsync(int id)
        {
            return await FindByCondition(category => category.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Category?> GetBySlugAsync(string slug)
        {
            return await FindByCondition(category => category.Slug.ToLower().Equals(slug.ToLower())).FirstOrDefaultAsync();
        }

        public async Task<Category?> GetWithSubcategoriesAsync(int id)
        {
            var spec = new CategoryWithSubcategorySpecification(id);
            return await Find(spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
