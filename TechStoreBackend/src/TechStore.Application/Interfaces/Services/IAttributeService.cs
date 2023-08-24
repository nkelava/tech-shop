using TechStore.Application.Models.Attribute;


namespace TechStore.Application.Interfaces.Services
{
    public interface IAttributeService
    {
        Task AddAsync(AttributeCreateModel attribute);
        Task DeleteAsync(int attributeId);
        Task UpdateAsync(AttributeUpdateModel attribute);

        Task<AttributeReadModel> GetAttributeByIdAsync(int id);
        Task<AttributeReadModel> GetAttributeByNameAsync(string name);

        Task<IList<AttributeReadModel>> GetAllAttributesAsync();

    }
}
