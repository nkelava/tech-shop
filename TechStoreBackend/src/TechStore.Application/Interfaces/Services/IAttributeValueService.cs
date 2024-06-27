using TechStore.Application.Models.AttributeValue;


namespace TechStore.Application.Interfaces.Services
{
    public interface IAttributeValueService
    {
        Task CreateAsync(AttributeValueCreateModel createModel);

        Task<AttributeValueReadModel?> UpdateAsync(int id, AttributeValueUpdateModel updateModel);
        Task<int?> DeleteAsync(int id);
        Task<AttributeValueReadModel?> GetByIdAsync(int id);

        Task<IEnumerable<AttributeValueReadModel>> GetAllAsync();
    }
}
