using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> GetProductBySlugAsync(string slug);

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsOnSaleAsync();
        Task<IEnumerable<Product>> GetNewProductsAsync();
        Task<IEnumerable<Product>> GetTopSellingProductsAsync();
        Task<IEnumerable<Product>> GetTopRatedProductsAsync();
        Task<IEnumerable<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId);
        Task<IEnumerable<Product>> GetProductsBySubcategorySlugAsync(string subcategorySlug);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string productName);
        Task<IEnumerable<Product>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo);
        Task<IEnumerable<Product>> GetProductsByRatingAsync(decimal rating);
    }
}
