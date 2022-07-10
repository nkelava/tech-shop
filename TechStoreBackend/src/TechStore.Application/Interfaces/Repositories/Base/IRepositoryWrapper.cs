

namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryWrapper
    {
        IBrandRepository Brand { get; }
        ICartRepository Cart { get; }
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ISubcategoryRepository Subcategory { get; }
        IWishlistRepository Wishlist { get; }

        Task SaveAsync();
    }
}
