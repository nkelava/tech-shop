using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task<Wishlist> GetWishlistByIdAsync(int subcategoryId);

        Task<IEnumerable<Wishlist>> GetAllWishlistsAsync();
    }
}
