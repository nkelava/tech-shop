using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.CartAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetByUserIdAsync(string id);
    }
}
