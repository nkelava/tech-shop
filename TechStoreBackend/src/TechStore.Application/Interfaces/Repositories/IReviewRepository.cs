using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
        Task<IEnumerable<Review>> GetByEmailAsync(string email);
        Task<IEnumerable<Review>> GetAllAsync();
        Task<IEnumerable<Review>> GetAllReportedReviewsAsync();
    }
}
