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

        public async Task AddReview(ReviewCreateModel reviewModel)
        {
            var product = await _repository.Product.GetProductByIdAsync(reviewModel.ProductId);
            var review = _mapper.Map<Review>(reviewModel);

            review.Product = product;
            _repository.Review.Add(review);
            //review =  _repository.Review.GetReviewsByEmail(reviewModel.Email).Where(r => r.ProductId.Equals(reviewModel.ProductId)).FirstOrDefault();

            if (review == null)
                return;

            product.AddReview(review);

            _repository.Product.Update(product);

            await _repository.SaveAsync();
        }

        public async Task DeleteReview(int reviewId)
        {
            var review = _repository.Review.FindById(reviewId);

            _repository.Review.Delete(review);
            await _repository.SaveAsync();
        }

        public IList<ReviewReadModel> GetReviewsByProductId(int productId)
        {
            var reviews = _repository.Review.GetReviewsByProductId(productId);
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }

        public IList<ReviewReadModel> GetReviewsByEmail(string email)
        {
            var reviews = _repository.Review.GetReviewsByEmail(email);
            var reviewsModel = _mapper.Map<IList<ReviewReadModel>>(reviews);

            return reviewsModel;
        }
    }
}
