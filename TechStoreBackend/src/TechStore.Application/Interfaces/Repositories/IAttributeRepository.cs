using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;

namespace TechStore.Application.Interfaces.Repositories
{
    public interface IAttributeRepository : IRepository<ProductAttribute>
    {
        Task<ProductAttribute> GetAttributeByIdAsync(int id);
        Task<ProductAttribute> GetAttributeByNameAsync(string name);

        Task<IEnumerable<ProductAttribute>> GetAllAttributesAsync();
    }
}
