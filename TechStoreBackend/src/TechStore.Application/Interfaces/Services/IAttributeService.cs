using TechStore.Application.Models.Attribute;


namespace TechStore.Application.Interfaces.Services
{
    public interface IAttributeService
    {
        Task CreateAsync(AttributeCreateModel attribute);
        Task DeleteAsync(int attributeId);
        Task UpdateAsync(AttributeUpdateModel attribute);

        Task<AttributeReadModel> GetAttributeByIdAsync(int id);
        Task<AttributeReadModel> GetAttributeByNameAsync(string name);

        Task<IEnumerable<AttributeReadModel>> GetAllAttributesAsync();

    }
}
