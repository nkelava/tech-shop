using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Specifications.ProductSpecification
{
    public class ProductWithReviewsSpecification : BaseSpecification<Product>
    {
        public ProductWithReviewsSpecification()
        {
            AddInclude(p => p.Reviews);
        }

        public ProductWithReviewsSpecification(int productId)
            : base(p => p.Id.Equals(productId))
        {
            AddInclude(p => p.Reviews);
        }

        public ProductWithReviewsSpecification(string slug)
            : base(p => p.Slug.ToLower().Equals(slug.ToLower()))
        {
            AddInclude(p => p.Reviews);
        }
    }
}
