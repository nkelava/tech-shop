using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IList<Review>> GetReviewsByProductIdAsync(int productId);
        Task<IList<Review>> GetReviewsByEmailAsync(string email);
        Task<IList<Review>> GetAllReviewsAsync();
    }
}
