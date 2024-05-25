using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IAttributeValueRepository : IRepository<ProductAttributeValue>
    {
        Task<ProductAttributeValue> GetByIdAsync(int id);

        Task<IEnumerable<ProductAttributeValue>> GetAllAsync();
    }
}
