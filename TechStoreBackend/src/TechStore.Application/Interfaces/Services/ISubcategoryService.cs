using TechStore.Application.Models.Subcategory;


namespace TechStore.Application.Interfaces.Services
{
    public interface ISubcategoryService
    {
        Task AddAsync(SubcategoryCreateModel subcategory);
        Task DeleteAsync(int subcategoryId);
        Task UpdateAsync(SubcategoryUpdateModel subcategory);

        Task<SubcategoryReadModel> GetSubcategoryByIdAsync(int subcategoryId);
        
        Task<IEnumerable<SubcategoryReadModel>> GetAllSubcategoriesAsync();
    }
}
