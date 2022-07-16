using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> GetProductBySlugAsync(string slug);

        Task<IList<Product>> GetAllProductsAsync();
        Task<IList<Product>> GetProductsOnSaleAsync();
        Task<IList<Product>> GetNewProductsAsync();
        Task<IList<Product>> GetTopSellingProductsAsync();
        Task<IList<Product>> GetTopRatedProductsAsync();
        Task<IList<Product>> GetProductsByBrandIdAsync(int brandId);
        Task<IList<Product>> GetProductsByBrandNameAsync(string brandName);
        Task<IList<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId);
        Task<IList<Product>> GetProductsBySubcategoryNameAsync(string subcategoryName);
        //Task<IEnumerable<Product>> GetProductsByPropertyIdAsync(int propertyId);
        //Task<IEnumerable<Product>> GetProductsByPropertyNameAsync(string propertyName);
        Task<IList<Product>> GetProductsByNameAsync(string productName);
        Task<IList<Product>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo);
        Task<IList<Product>> GetProductsByRatingAsync(decimal rating);
    }
}
