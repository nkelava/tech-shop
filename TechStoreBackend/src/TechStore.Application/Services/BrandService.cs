using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Brand;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Services
{
    public class BrandService : IBrandService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public BrandService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateBrand(BrandCreateModel brandModel)
        {
            var brand = _mapper.Map<Brand>(brandModel);

            _repository.Brand.CreateBrand(brand);
            await _repository.SaveAsync();
        }

        public async Task UpdateBrand(BrandUpdateModel brandModel)
        {
            var brand = _mapper.Map<Brand>(brandModel);
            _repository.Brand.UpdateBrand(brand);

            await _repository.SaveAsync();
        }

        public async Task DeleteBrand(int brandId)
        {
            var brand = await _repository.Brand.GetBrandByIdAsync(brandId);

            _repository.Brand.DeleteBrand(brand);
            await _repository.SaveAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int brandId)
        {
            var brand = await _repository.Brand.GetBrandByIdAsync(brandId);

            return brand;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            var brands = await _repository.Brand.GetAllBrandsAsync();

            return brands;
        }
    }
}
