using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.AttributeValue;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Services
{
    public class AttributeValueService: IAttributeValueService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AttributeValueService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(AttributeValueCreateModel attributeValueModel)
        {
            var attributeValue = _mapper.Map<ProductAttributeValue>(attributeValueModel);

            _repository.AttributeValue.Add(attributeValue);
            await _repository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var attributeValue = await _repository.AttributeValue.GetByIdAsync(id);

            if (attributeValue is null) return 0;

            _repository.AttributeValue.Delete(attributeValue);
            await _repository.SaveAsync();

            return attributeValue.Id;
        }

        public async Task UpdateAsync(AttributeValueUpdateModel attributeValueModel)
        {
            var attributeValue = _mapper.Map<ProductAttributeValue>(attributeValueModel);

            _repository.AttributeValue.Update(attributeValue);

            await _repository.SaveAsync();
        }

        public async Task<AttributeValueReadModel> GetByIdAsync(int id)
        {
            var attributeValue = await _repository.AttributeValue.GetByIdAsync(id);
            var attributeValueModel = _mapper.Map<AttributeValueReadModel>(attributeValue);

            return attributeValueModel;
        }


        public async Task<IEnumerable<AttributeValueReadModel>> GetAllAsync()
        {
            var attributeValues = await _repository.AttributeValue.GetAllAsync();
            var attributeValuesReadModel = _mapper.Map<IList<AttributeValueReadModel>>(attributeValues);

            return attributeValuesReadModel;
        }
    }
}
