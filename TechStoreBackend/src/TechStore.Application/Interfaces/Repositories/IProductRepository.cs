using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetByIdAsync(int productId);
        Task<Product?> GetByIdWithoutSubcategoryAsync(int id);
        Task<Product?> GetBySlugAsync(string slug);

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetHotOffersAsync();
        Task<IEnumerable<Product>> GetNewProductsAsync();
        Task<IEnumerable<Product>> GetTopSellingProductsAsync();
        Task<IEnumerable<Product>> GetTopRatedProductsAsync();
        Task<IEnumerable<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId);
        Task<IEnumerable<Product>> GetProductsBySubcategorySlugAsync(string subcategorySlug);
        Task<IEnumerable<Product>> SearchProductsAsync(string search);
        Task<IEnumerable<Product>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo);
        Task<IEnumerable<Product>> GetProductsByRatingAsync(decimal rating);
    }
}
