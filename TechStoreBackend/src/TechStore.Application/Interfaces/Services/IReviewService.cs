using TechStore.Application.Models.Review;


namespace TechStore.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task AddReview(ReviewCreateModel review);
        Task DeleteReview(int reviewId);

        IList<ReviewReadModel> GetReviewsByProductId(int productId);
        IList<ReviewReadModel> GetReviewsByEmail(string email);
    }
}
