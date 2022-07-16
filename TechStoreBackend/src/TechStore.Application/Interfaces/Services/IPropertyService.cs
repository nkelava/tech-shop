using TechStore.Application.Models.Property;


namespace TechStore.Application.Interfaces.Services
{
    public interface IPropertyService
    {
        Task AddAsync(PropertyCreateModel property);
        Task DeleteAsync(int propertyId);
        Task UpdateAsync(PropertyUpdateModel property);

        Task<PropertyReadModel> GetPropertyByIdAsync(int id);
        Task<PropertyReadModel> GetPropertyByNameAsync(string name);
    }
}
