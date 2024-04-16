using TechStore.Application.Models.Review;


namespace TechStore.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task CreateAsync(ReviewCreateModel review);
        Task DeleteAsync(int reviewId);

        Task<IEnumerable<ReviewReadModel>> GetReviewsByProductIdAsync(int productId);
        Task<IEnumerable<ReviewReadModel>> GetReviewsByEmailAsync(string email);
        Task<IEnumerable<ReviewReadModel>> GetAllReviewsAsync();
    }
}
