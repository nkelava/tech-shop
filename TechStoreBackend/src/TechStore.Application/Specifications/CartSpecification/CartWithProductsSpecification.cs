using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.Cart;


namespace TechStore.Application.Specifications.CartSpecification
{
    public class CartWithProductsSpecification : BaseSpecification<Cart>
    {
        public CartWithProductsSpecification(string username)
           : base(c => c.Username.ToLower() == username.ToLower())
        {
            AddInclude(c => c.Products);
        }

        public CartWithProductsSpecification(int cartId)
            : base(c => c.Id == cartId)
        {
            AddInclude(c => c.Products);
        }
    }
}
