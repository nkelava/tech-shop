using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.CartSpecification;
using TechStore.Domain.Entities.Cart;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(TechStoreContext techStoreContext)
            : base(techStoreContext) { }


        public async Task<Cart> GetByEmailAsync(string email)
        {
            var spec = new CartWithProductsSpecification(email);

            return Find(spec).FirstOrDefault();
        }
    }
}
