using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Infrastructure.Data;


namespace TechStore.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TechStoreContext _repositoryContext;

        public Repository(TechStoreContext repositoryContext)
        {
            _repositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }

        public void Create(T entity)
        {
            _repositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _repositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _repositoryContext.Set<T>().Where(expression).AsNoTracking();
        }
    }
}
