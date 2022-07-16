using TechStore.Application.Models.Product;


namespace TechStore.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task AddAsync(ProductCreateModel product);
        Task DeleteAsync(int productId);
        Task UpdateAsync(ProductUpdateModel product);

        Task<ProductReadModel> GetProductByIdAsync(int productId);
        Task<ProductReadModel> GetProductBySlugAsync(string slug);

        Task<IList<ProductReadModel>> GetAllProductsAsync();
        Task<IList<ProductReadModel>> GetProductsOnSaleAsync();
        Task<IList<ProductReadModel>> GetNewProductsAsync();
        Task<IList<ProductReadModel>> GetTopSellingProductsAsync();
        Task<IList<ProductReadModel>> GetTopRatedProductsAsync();
        Task<IList<ProductReadModel>> GetProductsByBrandIdAsync(int brandId);
        Task<IList<ProductReadModel>> GetProductsByBrandNameAsync(string brandName);
        Task<IList<ProductReadModel>> GetProductsBySubcategoryIdAsync(int subcategoryId);
        Task<IList<ProductReadModel>> GetProductsBySubcategoryNameAsync(string subcategoryName);
        Task<IList<ProductReadModel>> GetProductsByNameAsync(string productsName);
        Task<IList<ProductReadModel>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo);
        Task<IList<ProductReadModel>> GetProductsByRatingAsync(decimal rating);
    }
}
