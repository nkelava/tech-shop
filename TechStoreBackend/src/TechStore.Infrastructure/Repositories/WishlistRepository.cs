using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.WishlistSpecification;
using TechStore.Domain.Entities.Wishlist;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(TechStoreContext techStoreContext) : base(techStoreContext) { }

        public async Task<Wishlist> GetWishlistByUsernameAsync(string username)
        {
            var spec = new WishlistWithProductsSpecification(username);
            return (await Find(spec)).FirstOrDefault();
        }
    }
}
