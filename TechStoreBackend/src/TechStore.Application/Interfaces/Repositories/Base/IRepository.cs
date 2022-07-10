using System.Linq.Expressions;
using TechStore.Application.Interfaces.Specifications;


namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);

        bool Contains(ISpecification<T> specification);
        bool Contains(Expression<Func<T, bool>> predicate);

        int Count(ISpecification<T> specification);
        int Count(Expression<Func<T, bool>> predicate);

        T FindById(int id);
        IQueryable<T> Find(ISpecification<T> specification);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
