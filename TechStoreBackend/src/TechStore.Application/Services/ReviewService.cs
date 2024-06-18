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

            if (product is null || review is null) throw new ArgumentNullException();
            
            review.Email = email;
            review.Product = product;
            
            _repository.Review.Add(review);
            product.AddReview(review);
            _repository.Product.Update(product);

            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int reviewId)
        {
            var review = _repository.Review.FindById(reviewId);

            _repository.Review.Delete(review);
            await _repository.SaveAsync();
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

        public async Task<IEnumerable<ReviewReadModel>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await _repository.Review.GetReviewsByProductIdAsync(productId);
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }

        public async Task<IEnumerable<ReviewReadModel>> GetReviewsByEmailAsync(string email)
        {
            var reviews = await _repository.Review.GetReviewsByEmailAsync(email);
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }

        public async Task<IEnumerable<ReviewReadModel>> GetAllReviewsAsync()
        {
            var reviews = await _repository.Review.GetAllReviewsAsync();
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
