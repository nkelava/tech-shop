using TechStore.Application.Models.Subcategory;


namespace TechStore.Application.Interfaces.Services
{
    public interface ISubcategoryService
    {
        Task CreateAsync(SubcategoryCreateModel createModel);
        
        Task<SubcategoryReadModel?> UpdateAsync(int id, SubcategoryUpdateModel updateModel);
        Task<int?> DeleteAsync(int id);
        Task<SubcategoryReadModel?> GetByIdAsync(int id);
        Task<SubcategoryReadModel?> GetBySlugAsync(string slug);
        
        Task<IEnumerable<SubcategoryReadModel>> GetAllAsync();
    }
}
