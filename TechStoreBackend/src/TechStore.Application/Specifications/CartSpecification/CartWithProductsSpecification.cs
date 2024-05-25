using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.Cart;


namespace TechStore.Application.Specifications.CartSpecification
{
    public class CartWithProductsSpecification : BaseSpecification<Cart>
    {
        public CartWithProductsSpecification(string email)
           : base(c => c.Email.ToLower().Equals(email.ToLower()))
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
