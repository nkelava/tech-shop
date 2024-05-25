using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetSubcategoryByIdAsync(int subcategoryId);
        Task<Subcategory> GetSubcategoryBySlugAsync(string subcategorySlug);

        Task<IEnumerable<Subcategory>> GetAllSubcategoriesAsync();
    }
}
