using TechStore.Application.Models.Subcategory;


namespace TechStore.Application.Interfaces.Services
{
    public interface ISubcategoryService
    {
        Task CreateAsync(SubcategoryCreateModel subcategory);
        Task DeleteAsync(int subcategoryId);
        Task UpdateAsync(SubcategoryUpdateModel subcategory);

        Task<SubcategoryReadModel> GetSubcategoryByIdAsync(int subcategoryId);
        Task<SubcategoryReadModel> GetSubcategoryBySlugAsync(string subcategorySlug);
        
        Task<IEnumerable<SubcategoryReadModel>> GetAllSubcategoriesAsync();
    }
}
