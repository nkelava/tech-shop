using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.Cart;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetByUsernameAsync(string username);
    }
}
