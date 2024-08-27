using TechStore.Application.Models.Order;
using TechStore.Domain.Enums.Order;


namespace TechStore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderCreateModel createModel);
        Task UpdateAsync(OrderUpdateModel updateModel);

        Task<bool> DeleteAsync(int id);
        Task<int?> UpdateOrderStatusAsync(OrderUpdateStatusModel updateModel);
        Task<int?> UpdatePaymentStatusAsync(int orderId, PaymentStatus paymentStatus);
        Task<OrderReadModel?> GetByIdAsync(int id);
        Task<OrderReadModel?> GetBySessionIdAsync(string sessionId);

        Task<IEnumerable<OrderReadModel>> GetAllAsync();
        Task<IEnumerable<OrderReadModel>> GetAllAsync(string userId);
    }
}
