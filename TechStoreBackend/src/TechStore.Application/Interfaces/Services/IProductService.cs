using TechStore.Application.Models.Product;


namespace TechStore.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateModel product);
        Task DeleteAsync(int productId);
        Task UpdateAsync(ProductUpdateModel product);

        Task<ProductReadModel> GetProductByIdAsync(int productId);
        Task<ProductReadModel> GetProductBySlugAsync(string slug);

        Task<IEnumerable<ProductReadModel>> GetAllProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetProductsOnSaleAsync();
        Task<IEnumerable<ProductReadModel>> GetNewProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetTopSellingProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetTopRatedProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetProductsBySubcategoryIdAsync(int subcategoryId);
        Task<IEnumerable<ProductReadModel>> GetProductsBySubcategorySlugAsync(string subcategorySlug);
        Task<IEnumerable<ProductReadModel>> GetProductsByNameAsync(string productsName);
        Task<IEnumerable<ProductReadModel>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo);
        Task<IEnumerable<ProductReadModel>> GetProductsByRatingAsync(decimal rating);
    }
}
