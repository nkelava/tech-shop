using TechStore.Application.Models.Attribute;


namespace TechStore.Application.Interfaces.Services
{
    public interface IAttributeService
    {
        Task CreateAsync(AttributeCreateModel attribute);
        Task UpdateAsync(AttributeUpdateModel attribute);

        Task<int> DeleteAsync(int attributeId);
        Task<AttributeReadModel> GetByIdAsync(int id);
        Task<AttributeReadModel> GetByNameAsync(string name);

        Task<IEnumerable<AttributeReadModel>> GetAllAsync();

    }
}
