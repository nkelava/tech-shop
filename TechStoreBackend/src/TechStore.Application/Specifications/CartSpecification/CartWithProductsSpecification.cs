using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.CartAggregate;


namespace TechStore.Application.Specifications.CartSpecification
{
    public class CartWithProductsSpecification : BaseSpecification<Cart>
    {
        public CartWithProductsSpecification(string userId)
           : base(c => c.ApplicationUser != null && c.ApplicationUser.Id.Equals(userId))
        {
            AddInclude(c => c.Products);
        }

        public CartWithProductsSpecification(int cartId)
            : base(c => c.Id.Equals(cartId))
        {
            AddInclude(c => c.Products);
        }
    }
}
