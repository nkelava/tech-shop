using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Attribute;
using TechStore.Domain.Entities.ProductAggregate;

namespace TechStore.Application.Services
{
    public class AttributeService : IAttributeService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AttributeService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(AttributeCreateModel attributeModel)
        {
            var attribute = _mapper.Map<ProductAttribute>(attributeModel);

            _repository.Attribute.Add(attribute);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attribute = await _repository.Attribute.GetAttributeByIdAsync(id);

            if (attribute is null) return;

            _repository.Attribute.Delete(attribute);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(AttributeUpdateModel attributeModel)
        {
            var attribute = _mapper.Map<ProductAttribute>(attributeModel);

            _repository.Attribute.Update(attribute);

            await _repository.SaveAsync();
        }

        public async Task<AttributeReadModel> GetAttributeByIdAsync(int id)
        {
            var attribute = await _repository.Attribute.GetAttributeByIdAsync(id);
            var attributeModel = _mapper.Map<AttributeReadModel>(attribute);

            return attributeModel;
        }
        
        public async Task<AttributeReadModel> GetAttributeByNameAsync(string name)
        {
            var attribute = await _repository.Attribute.GetAttributeByNameAsync(name);
            var attributeModel = _mapper.Map<AttributeReadModel>(attribute);

            return attributeModel;
        }

        public async Task<IEnumerable<AttributeReadModel>> GetAllAttributesAsync()
        {
            var attributes = await _repository.Attribute.GetAllAttributesAsync();
            var attributesReadModel = _mapper.Map<IList<AttributeReadModel>>(attributes);

            return attributesReadModel;
        }
    }
}
