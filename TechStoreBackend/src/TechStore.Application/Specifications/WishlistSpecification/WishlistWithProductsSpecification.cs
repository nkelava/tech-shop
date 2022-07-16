using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Application.Specifications.WishlistSpecification
{
    public class WishlistWithProductsSpecification: BaseSpecification<Wishlist>
    {
        public WishlistWithProductsSpecification(int wishlistId)
      : base(w => w.Id.Equals(wishlistId))
        {
            AddInclude(w => w.Products);
        }

        public WishlistWithProductsSpecification(string email) 
            : base(w => w.Email.ToLower().Equals(email.ToLower()))
        {
            AddInclude(w => w.Products);
        }
    }
}
