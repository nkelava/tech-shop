using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.WishlistAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task<Wishlist?> GetByUserIdAsync(string id);
    }
}
