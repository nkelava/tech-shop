using TechStore.Application.Models.Category;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task CreateCategory(CategoryCreateModel category);
        Task UpdateCategory(CategoryUpdateModel category);
        Task DeleteCategory(int categoryId);

        Task<Category> GetCategoryByIdAsync(int categoryId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
