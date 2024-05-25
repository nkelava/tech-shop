using TechStore.Application.Models.PromoCode;


namespace TechStore.Application.Interfaces.Services
{
    public interface IPromoCodeService
    {
        Task CreateAsync(PromoCodeCreateModel promoCode);
        Task UpdateAsync(PromoCodeUpdateModel promoCode);
        
        Task<int> DeleteAsync(int promoCodeId);
        Task<PromoCodeReadModel> GetByIdAsync(int id);
        Task<PromoCodeReadModel> GetByCodeAsync(string code);

        Task<IEnumerable<PromoCodeReadModel>> GetAllAsync();
    }
}
