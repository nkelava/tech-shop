using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<Review>> GetReviewsByProductIdAsync(int productId)
        {
            var spec = new ReviewsWithProductSpecification(productId);
            var reviews = await Find(spec).ToListAsync();

            return reviews;
        }

        public async Task<IList<Review>> GetReviewsByEmailAsync(string email)
        {
            var spec = new ReviewsWithProductSpecification(email);
            var reviews = await Find(spec).ToListAsync();

            return reviews;
        }

        public async Task<IList<Review>> GetAllReviewsAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
 