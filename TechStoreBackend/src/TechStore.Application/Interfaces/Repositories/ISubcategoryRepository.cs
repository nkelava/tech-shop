using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetSubcategoryByIdAsync(int subcategoryId);

        Task<IEnumerable<Subcategory>> GetAllSubcategoriesAsync();
    }
}
