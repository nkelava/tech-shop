using TechStore.Application.Models.AttributeValue;


namespace TechStore.Application.Interfaces.Services
{
    public interface IAttributeValueService
    {
        Task CreateAsync(AttributeValueCreateModel attributeValueModel);
        Task UpdateAsync(AttributeValueUpdateModel attributeValueModel);

        Task<int> DeleteAsync(int id);
        Task<AttributeValueReadModel> GetByIdAsync(int id);

        Task<IEnumerable<AttributeValueReadModel>> GetAllAsync();
    }
}
