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

        public async Task AddAsync(BrandCreateModel brandModel)
        {
            var brand = _mapper.Map<Brand>(brandModel);

            _repository.Brand.Add(brand);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int brandId)
        {
            var brand = await _repository.Brand.GetBrandByIdAsync(brandId);

            if (brand == null)
                return;

            _repository.Brand.Delete(brand);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(BrandUpdateModel brandModel)
        {
            var brand = _mapper.Map<Brand>(brandModel);
            _repository.Brand.Update(brand);

            await _repository.SaveAsync();
        }

        public async Task<BrandReadModel> GetBrandByIdAsync(int brandId)
        {
            var brand = await _repository.Brand.GetBrandByIdAsync(brandId);
            var brandModel = _mapper.Map<BrandReadModel>(brand);

            return brandModel;
        }

        public async Task<IEnumerable<BrandReadModel>> GetAllBrandsAsync()
        {
            var brands = await _repository.Brand.GetAllBrandsAsync();
            var brandsModel = _mapper.Map<IEnumerable<BrandReadModel>>(brands);

            return brandsModel;
        }
    }
}
