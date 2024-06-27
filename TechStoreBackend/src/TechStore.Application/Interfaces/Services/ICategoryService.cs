using TechStore.Application.Models.Category;


namespace TechStore.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryReadModel?> CreateAsync(CategoryCreateModel category);
        Task<CategoryReadModel?> UpdateAsync(int id, CategoryUpdateModel category);
        Task<int?> DeleteAsync(int id);
        Task<CategoryReadModel?> GetByIdAsync(int id);
        Task<CategoryReadModel?> GetBySlugAsync(string slug);
        Task<CategoryWithSubcategoriesModel?> GetWithSubcategoriesAsync(string slug);


        Task<IEnumerable<CategoryReadModel>> GetAllAsync();
    }
}
