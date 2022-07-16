using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Property;
using TechStore.Domain.Entities;


namespace TechStore.Application.Services
{
    public class PropertyService : IPropertyService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public PropertyService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(PropertyCreateModel propertyModel)
        {
            var property = _mapper.Map<Property>(propertyModel);

            _repository.Property.Add(property);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var property = await _repository.Property.GetPropertyByIdAsync(id);

            if (property == null)
                return;

            _repository.Property.Delete(property);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(PropertyUpdateModel propertyModel)
        {
            var property = _mapper.Map<Property>(propertyModel);

            _repository.Property.Update(property);

            await _repository.SaveAsync();
        }

        public async Task<PropertyReadModel> GetPropertyByIdAsync(int id)
        {
            var property = await _repository.Property.GetPropertyByIdAsync(id);
            var propertyModel = _mapper.Map<PropertyReadModel>(property);

            return propertyModel;
        }
        
        public async Task<PropertyReadModel> GetPropertyByNameAsync(string name)
        {
            var property = await _repository.Property.GetPropertyByNameAsync(name);
            var propertyModel = _mapper.Map<PropertyReadModel>(property);

            return propertyModel;
        }
    }
}
