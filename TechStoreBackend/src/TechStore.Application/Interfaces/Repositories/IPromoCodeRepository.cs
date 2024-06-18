using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IPromoCodeRepository: IRepository<PromoCode>
    {
        Task<PromoCode?> GetByIdAsync(int id);
        Task<PromoCode?> GetByCodeAsync(string code);
        
        Task<IEnumerable<PromoCode>> GetAllAsync();
    }
}
