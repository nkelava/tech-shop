using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> GetBySlugAsync(string slug);
        Task<Category?> GetWithSubcategoriesAsync(int id);

        Task<IEnumerable<Category>> GetAllAsync();
    }
}
