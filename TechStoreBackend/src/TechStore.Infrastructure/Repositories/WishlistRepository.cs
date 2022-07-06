using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.Wishlist;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(TechStoreContext techStoreContext) : base(techStoreContext) { }

        public async Task<Wishlist> GetWishlistByIdAsync(int wishlistId)
        {
            return await FindByCondition(wishlist => wishlist.Id.Equals(wishlistId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Wishlist>> GetAllWishlistsAsync()
        {
            return await FindAll().ToListAsync();
        }

    }
}
