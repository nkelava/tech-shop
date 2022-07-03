using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Infrastructure.Data;


namespace TechStore.Infrastructure.Repositories.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TechStoreContext _repositoryContext;
        private IBrandRepository _brand;

        public IBrandRepository Brand
        {
            get
            {
                if (_brand == null)
                {
                    _brand = new BrandRepository(_repositoryContext);
                }

                return _brand;
            }
        }

        public RepositoryWrapper(TechStoreContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
