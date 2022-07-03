using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(TechStoreContext techStoreContext) : base(techStoreContext) { }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await FindByCondition(category => category.Id.Equals(categoryId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
