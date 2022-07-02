using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Interfaces.Services;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Services
{
    public class BrandService : IBrandService
    {
        public readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }


        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllBrands();

            return brands;
        }
    }
}
