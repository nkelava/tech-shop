using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        IList<Review> GetReviewsByProductId(int productId);
        IList<Review> GetReviewsByEmail(string email);
    }
}
