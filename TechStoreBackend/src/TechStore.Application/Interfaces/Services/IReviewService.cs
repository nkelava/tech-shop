using TechStore.Application.Models.Review;


namespace TechStore.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task CreateAsync(string email, ReviewCreateModel review);
        Task DeleteAsync(int reviewId);

        Task<int> ReportReviewAsync(int reviewId, bool isReportable);

        Task<IEnumerable<ReviewReadModel>> GetReviewsByProductIdAsync(int productId);
        Task<IEnumerable<ReviewReadModel>> GetReviewsByEmailAsync(string email);
        Task<IEnumerable<ReviewReadModel>> GetAllReviewsAsync();
        Task<IEnumerable<ReviewReadModel>> GetAllReportedReviewsAsync();
    }
}
