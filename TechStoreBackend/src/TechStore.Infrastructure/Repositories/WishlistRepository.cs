using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.WishlistSpecification;
using TechStore.Domain.Entities.WishlistAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(TechStoreContext techStoreContext) 
            : base(techStoreContext) { }


        public async Task<Wishlist?> GetByUserIdAsync(string id)
        {
            var spec = new WishlistWithProductsSpecification(id);
            return await Find(spec).FirstOrDefaultAsync();
        }
    }
}
