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

        public void Add(T entity)
        {
            _repositoryContext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _repositoryContext.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _repositoryContext.Set<T>().RemoveRange(entities);
        }


        public void Update(T entity)
        {
            _repositoryContext.Entry(entity).State = EntityState.Modified;
        }
        public bool Contains(ISpecification<T> specification )
        {
            return Count(specification) > 0 ? true : false;
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return Count(predicate) > 0 ? true : false;
        }

        public int Count(ISpecification<T> specification)
        {
            return ApplySpecification(specification).Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _repositoryContext.Set<T>().Where(predicate).Count();
        }

        public T FindById(int id)
        {
            return _repositoryContext.Set<T>().Find(id);
        }
        public IQueryable<T> Find(ISpecification<T> specification)
        {
            return ApplySpecification(specification);
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
