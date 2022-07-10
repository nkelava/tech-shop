using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.ReviewSpecification;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(TechStoreContext techStoreContext)
            : base(techStoreContext) { }

        public IList<Review> GetReviewsByProductId(int productId)
        {
            var spec = new ReviewsWithProductSpecification(productId);
            var reviews = Find(spec).ToList();

            return reviews;
        }

        public IList<Review> GetReviewsByEmail(string email)
        {
            var spec = new ReviewsWithProductSpecification(email);
            var reviews = Find(spec).ToList();

            return reviews;
        }
    }
}
