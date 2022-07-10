using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> GetCategoryWithSubcategoriesAsync(int categoryId);

        Task<IList<Category>> GetAllCategoriesAsync();
    }
}
