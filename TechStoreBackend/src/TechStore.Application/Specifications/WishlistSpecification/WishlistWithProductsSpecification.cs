using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.WishlistAggregate;


namespace TechStore.Application.Specifications.WishlistSpecification
{
    public class WishlistWithProductsSpecification: BaseSpecification<Wishlist>
    {
        public WishlistWithProductsSpecification(int wishlistId)
      : base(w => w.Id.Equals(wishlistId))
        {
            AddInclude(w => w.Products);
        }

        public WishlistWithProductsSpecification(string userId) 
            : base(w => w.ApplicationUserId.Equals(userId))
        {
            AddInclude(w => w.Products);
        }
    }
}
