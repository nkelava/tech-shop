using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.WishlistSpecification;
using TechStore.Domain.Entities.Wishlist;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(TechStoreContext techStoreContext) 
            : base(techStoreContext) { }

        public Wishlist GetByEmailAsync(string email)
        {
            var spec = new WishlistWithProductsSpecification(email);
            var wishlist = Find(spec).FirstOrDefault();

            return wishlist;
        }
    }
}
