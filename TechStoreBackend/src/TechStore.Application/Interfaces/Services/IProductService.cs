using TechStore.Application.Models.Product;


namespace TechStore.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateModel product);

        Task<int?> DeleteAsync(int id);
        Task<ProductReadModel?> UpdateAsync(int id, ProductUpdateModel product);
        Task<ProductReadModel?> GetByIdAsync(int id);
        Task<ProductReadModel?> GetBySlugAsync(string slug);
        Task<ProductReadModel?> AddSpecificationAsync(int id, List<ProductAttributeSetModel> productAttributes);

        Task<IEnumerable<ProductReadModel>> GetAllProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetHotOffersAsync();
        Task<IEnumerable<ProductReadModel>> GetNewProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetTopSellingProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetTopRatedProductsAsync();
        Task<IEnumerable<ProductReadModel>> SearchProductsAsync(string search);
        Task<IEnumerable<ProductReadModel>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo);
        Task<IEnumerable<ProductReadModel>> GetProductsByRatingAsync(decimal rating);
        Task<IEnumerable<ProductReadModel>?> GetProductsBySubcategoryIdAsync(int subcategoryId);
        Task<IEnumerable<ProductReadModel>?> GetProductsBySubcategorySlugAsync(string subcategorySlug);
    }
}
