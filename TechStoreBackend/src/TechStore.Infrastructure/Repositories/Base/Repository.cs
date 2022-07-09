using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Specifications;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Specifications;


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
            _repositoryContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }

        public async Task<int> Count(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<IList<T>> Find(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public IQueryable<T> FindAll()
        {
            return _repositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _repositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_repositoryContext.Set<T>().AsQueryable(), spec);
        }
    }
}
