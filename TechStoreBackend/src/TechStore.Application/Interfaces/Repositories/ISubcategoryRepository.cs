using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory?> GetByIdAsync(int id);
        Task<Subcategory?> GetBySlugAsync(string slug);

        Task<IEnumerable<Subcategory?>> GetAllAsync();
    }
}
