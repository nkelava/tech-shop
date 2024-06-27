using TechStore.Application.Models.Review;


namespace TechStore.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task CreateAsync(string email, ReviewCreateModel review);

        Task<int?> DeleteAsync(int id);
        Task<ReviewReadModel?> ReportReviewAsync(int id, bool isReportable);

        Task<IEnumerable<ReviewReadModel>?> GetByProductIdAsync(int productId);
        Task<IEnumerable<ReviewReadModel>> GetByEmailAsync(string email);
        Task<IEnumerable<ReviewReadModel>> GetAllAsync();
        Task<IEnumerable<ReviewReadModel>> GetAllReportedReviewsAsync();
    }
}
