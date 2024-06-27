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


        public async Task CreateAsync(AttributeCreateModel createModel)
        {
            var attribute = _mapper.Map<ProductAttribute>(createModel);

            _repository.Attribute.Add(attribute);
            await _repository.SaveAsync();
        }

        public async Task<AttributeReadModel?> UpdateAsync(int id, AttributeUpdateModel updateModel)
        {
            var attribute = await _repository.Attribute.GetByIdAsync(id);

            if (attribute == null)
                return null;

            _mapper.Map(updateModel, attribute);
            _repository.Attribute.Update(attribute);
            await _repository.SaveAsync();

            var attributeModel = _mapper.Map<AttributeReadModel>(attribute);
            return attributeModel;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var attribute = await _repository.Attribute.GetByIdAsync(id);

            if (attribute == null)
                return null;

            _repository.Attribute.Delete(attribute);
            await _repository.SaveAsync();

            return attribute.Id;
        }

        public async Task<AttributeReadModel?> GetByIdAsync(int id)
        {
            var attribute = await _repository.Attribute.GetByIdAsync(id);
            
            if (attribute == null)
                return null;

            var attributeModel = _mapper.Map<AttributeReadModel>(attribute);
            return attributeModel;
        }
        
        public async Task<AttributeReadModel?> GetByNameAsync(string name)
        {
            var attribute = await _repository.Attribute.GetByNameAsync(name);
            
            if (attribute == null)
                return null;
            
            var attributeModel = _mapper.Map<AttributeReadModel>(attribute);
            return attributeModel;
        }

        public async Task<IEnumerable<AttributeReadModel>> GetAllAsync()
        {
            var attributes = await _repository.Attribute.GetAllAsync();
            var attributesModel = _mapper.Map<IList<AttributeReadModel>>(attributes);

            return attributesModel;
        }
    }
}
