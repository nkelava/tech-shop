using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IAttributeRepository : IRepository<ProductAttribute>
    {
        Task<ProductAttribute> GetByIdAsync(int id);
        Task<ProductAttribute> GetByNameAsync(string name);

        Task<IEnumerable<ProductAttribute>> GetAllAsync();
    }
}
