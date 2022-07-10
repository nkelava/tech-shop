using TechStore.Application.Models.Category;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryCreateModel category);
        Task UpdateAsync(CategoryUpdateModel category);
        Task DeleteAsync(int categoryId);

        Task<CategoryReadModel> GetCategoryByIdAsync(int categoryId);
        Task<CategoryWithSubcategoriesModel> GetCategoryWithSubcategoriesAsync(int categoryId);

        Task<IList<CategoryReadModel>> GetAllCategoriesAsync();
    }
}
