using TechStore.Application.Models.Brand;


namespace TechStore.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task AddAsync(BrandCreateModel brand);
        Task DeleteAsync(int brandId);
        Task UpdateAsync(BrandUpdateModel brand);

        Task<BrandReadModel> GetBrandByIdAsync(int brandId);

        Task<IEnumerable<BrandReadModel>> GetAllBrandsAsync();
    }
}
