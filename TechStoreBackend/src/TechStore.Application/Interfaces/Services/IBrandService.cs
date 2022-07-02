using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrands();

        //Task<BrandDTO> Create(BrandDTO brand);
        //Task<BrandDTO> Delete(int brandId;
        //Task<BrandDTO> Get(int brandId);
        //Task<IList<BrandDTO>> GetBrands();
    }
}
