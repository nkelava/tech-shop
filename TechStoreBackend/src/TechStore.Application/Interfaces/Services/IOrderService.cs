using TechStore.Application.Models.Order;


namespace TechStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderCreateModel createModel);
        Task UpdateAsync(OrderUpdateModel updateModel);

        Task<bool> DeleteAsync(int id);
        Task<int?> UpdateOrderStatusAsync(OrderUpdateStatusModel updateModel);
        Task<OrderReadModel?> GetByIdAsync(int id);

        Task<IEnumerable<OrderReadModel>> GetAllAsync();
        Task<IEnumerable<OrderReadModel>> GetAllAsync(string email);
    }
}
