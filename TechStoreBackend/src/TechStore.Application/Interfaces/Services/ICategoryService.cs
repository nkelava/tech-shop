using TechStore.Application.Models.Category;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateModel category);
        Task DeleteAsync(int categoryId);

        Task<CategoryReadModel?> UpdateAsync(int id, CategoryUpdateModel category);
        Task<CategoryReadModel?> GetByIdAsync(int id);
        Task<CategoryReadModel> GetCategoryBySlugAsync(string categorySlug);
        Task<CategoryWithSubcategoriesModel> GetCategoryWithSubcategoriesAsync(string categorySlug);

        Task<IEnumerable<CategoryReadModel>> GetAllCategoriesAsync();
    }
}
