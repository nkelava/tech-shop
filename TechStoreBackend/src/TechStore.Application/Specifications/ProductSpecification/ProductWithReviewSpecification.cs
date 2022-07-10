using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Specifications.ProductSpecification
{
    public class ProductWithReviewSpecification : BaseSpecification<Product>
    {
        public ProductWithReviewSpecification(int productId)
            : base(p => p.Id.Equals(productId))
        {
            AddInclude(p => p.Reviews);
        }

        public ProductWithReviewSpecification(string slug)
            : base(p => p.Slug.ToLower().Equals(slug.ToLower()))
        {
            AddInclude(p => p.Reviews);
        }
    }
}
