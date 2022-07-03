using TechStore.Application.Models.Brand;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task CreateBrand(BrandCreateModel brand);
        Task UpdateBrand(BrandUpdateModel brand);
        Task DeleteBrand(int brandId);

        Task<Brand> GetBrandByIdAsync(int brandId);

        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}
