using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.Base;
using TechStore.Infrastructure.Data;


namespace TechStore.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly TechStoreContext _dbContext;

        public Repository(TechStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
