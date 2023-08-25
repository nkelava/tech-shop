using TechStore.Application.Models.Category;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryCreateModel category);
        Task UpdateAsync(CategoryUpdateModel category);
        Task DeleteAsync(int categoryId);

        Task<CategoryReadModel> GetCategoryBySlugAsync(string categorySlug);
        Task<CategoryWithSubcategoriesModel> GetCategoryWithSubcategoriesAsync(string categorySlug);

        Task<IList<CategoryReadModel>> GetAllCategoriesAsync();
    }
}
