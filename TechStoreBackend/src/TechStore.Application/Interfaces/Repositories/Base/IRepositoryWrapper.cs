

namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryWrapper
    {
        IBrandRepository Brand { get; }
        ICartRepository Cart { get; }
        ICategoryRepository Category { get; }
        INewsletterRepository Newsletter { get; }
        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        IPropertyRepository Property { get; }
        IReviewRepository Review { get; }
        ISubcategoryRepository Subcategory { get; }
        IWishlistRepository Wishlist { get; }

        Task SaveAsync();
    }
}
