using TechStore.Application.Models.Review;


namespace TechStore.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task AddReview(ReviewCreateModel review);
        Task DeleteReview(int reviewId);

        Task<IList<ReviewReadModel>> GetReviewsByProductIdAsync(int productId);
        Task<IList<ReviewReadModel>> GetReviewsByEmailAsync(string email);
        Task<IList<ReviewReadModel>> GetAllReviewsAsync();
    }
}
