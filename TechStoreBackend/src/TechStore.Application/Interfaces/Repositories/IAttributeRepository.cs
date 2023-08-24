using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Models.Product;
using TechStore.Domain.Entities;
using TechStore.Domain.Entities.ProductAggregate;

namespace TechStore.Application.Interfaces.Repositories
{
    public interface IAttributeRepository : IRepository<ProductAttribute>
    {
        Task<ProductAttribute> GetAttributeByIdAsync(int id);
        Task<ProductAttribute> GetAttributeByNameAsync(string name);

        Task<IList<ProductAttribute>> GetAllAttributesAsync();
    }
}
