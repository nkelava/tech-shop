using System.Linq.Expressions;


namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
