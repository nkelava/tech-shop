using TechStore.Application.Models.Brand;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task AddAsync(BrandCreateModel brand);
        Task DeleteAsync(int brandId);
        Task UpdateAsync(BrandUpdateModel brand);

        Task<Brand> GetBrandByIdAsync(int brandId);

        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}
