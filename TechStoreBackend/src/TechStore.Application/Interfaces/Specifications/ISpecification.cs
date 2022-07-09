using System.Linq.Expressions;


namespace TechStore.Application.Interfaces.Specifications
{
    public interface ISpecification<T>
    {
        bool isPagingEnabled { get; }
        int Skip { get; }
        int Take { get; }

        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
    }
}
