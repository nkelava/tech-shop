using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class PromoCodeRepository : Repository<PromoCode>, IPromoCodeRepository
    {
        public PromoCodeRepository(TechStoreContext techStoreContext) 
            : base(techStoreContext) { }

        public async Task<PromoCode?> GetByIdAsync(int id)
        {
            return await FindByCondition(pc => pc.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<PromoCode?> GetByCodeAsync(string code)
        {
            return await FindByCondition(pc => pc.Code.Equals(code)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PromoCode>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
