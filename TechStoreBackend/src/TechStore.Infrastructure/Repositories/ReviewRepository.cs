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

        public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId)
        {
            var spec = new ReviewsWithProductSpecification(productId);
            var reviews = await Find(spec).ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetByEmailAsync(string email)
        {
            var spec = new ReviewsWithProductSpecification(email);
            var reviews = await Find(spec).ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReportedReviewsAsync()
        {
            return await FindByCondition(r => r.IsReported).ToListAsync();
        }
    }
}
 