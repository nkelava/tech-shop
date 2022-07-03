using TechStore.Application.Models.Subcategory;


namespace TechStore.Application.Interfaces.Services
{
    public interface ISubcategoryService
    {
        Task CreateSubcategory(SubcategoryCreateModel subcategory);
        Task UpdateSubcategory(SubcategoryUpdateModel subcategory);
        Task DeleteSubcategory(int subcategoryId);

        Task<SubcategoryReadModel> GetSubcategoryByIdAsync(int subcategoryId);
        
        Task<IEnumerable<SubcategoryReadModel>> GetAllSubcategoriesAsync();
    }
}
