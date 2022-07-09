using System.Linq.Expressions;
using TechStore.Application.Interfaces.Specifications;


namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        Task<int> Count(ISpecification<T> spec);
        Task<IList<T>> Find(ISpecification<T> spec);
    }
}
