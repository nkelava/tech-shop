using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Review;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Services
{
    public class ReviewService : IReviewService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ReviewService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(string email, ReviewCreateModel reviewModel)
        {
            var product = await _repository.Product.GetProductByIdAsync(reviewModel.ProductId);
            var review = _mapper.Map<Review>(reviewModel);

            if (product == null)
                throw new ArgumentNullException($"Product with ID {reviewModel.ProductId} not found.");
            
            review.Email = email;
            review.Product = product;
            
            _repository.Review.Add(review);
            product.AddReview(review);
            _repository.Product.Update(product);

            await _repository.SaveAsync();
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var review = _repository.Review.FindById(id);

            if (review == null)
                return null;

            _repository.Review.Delete(review);
            await _repository.SaveAsync();

            return review.Id;
        }

        public async Task<ReviewReadModel?> ReportReviewAsync(int reviewId, bool isReported)
        {
            var review = _repository.Review.FindById(reviewId);

            if (review == null)
                return null;

            review.IsReported = isReported;
            _repository.Review.Update(review);
            await _repository.SaveAsync();

            var reviewModel = _mapper.Map<ReviewReadModel>(review);
            return reviewModel;
        }

        public async Task<IEnumerable<ReviewReadModel>?> GetByProductIdAsync(int productId)
        {
            var product = await _repository.Product.GetProductByIdAsync(productId);

            if (product == null)
                return null;

            var reviews = await _repository.Review.GetByProductIdAsync(productId);
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }

        public async Task<IEnumerable<ReviewReadModel>> GetByEmailAsync(string email)
        {
            var reviews = await _repository.Review.GetByEmailAsync(email);
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }

        public async Task<IEnumerable<ReviewReadModel>> GetAllAsync()
        {
            var reviews = await _repository.Review.GetAllAsync();
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }
        
        public async Task<IEnumerable<ReviewReadModel>> GetAllReportedReviewsAsync()
        {
            var reviews = await _repository.Review.GetAllReportedReviewsAsync();
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }
    }
}
