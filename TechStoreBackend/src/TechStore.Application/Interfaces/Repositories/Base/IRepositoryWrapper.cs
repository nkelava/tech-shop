

namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryWrapper
    {
        IAttributeRepository Attribute { get; }
        IAttributeValueRepository AttributeValue { get; }
        ICartRepository Cart { get; }
        ICategoryRepository Category { get; }
        INewsletterRepository Newsletter { get; }
        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        IProductAttributeSetRepository ProductAttributeSet { get; }
        IPromoCodeRepository PromoCode { get; }
        IReviewRepository Review { get; }
        ISubcategoryRepository Subcategory { get; }
        IWishlistRepository Wishlist { get; }

        Task SaveAsync();
    }
}
