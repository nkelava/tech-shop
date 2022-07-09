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


        public async Task<Cart> GetByUsernameAsync(string username)
        {
            var spec = new CartWithProductsSpecification(username);

            return (await Find(spec)).FirstOrDefault();
        }
    }
}
