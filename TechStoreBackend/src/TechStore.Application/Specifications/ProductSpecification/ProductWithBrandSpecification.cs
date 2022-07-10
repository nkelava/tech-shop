using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Specifications.ProductSpecification
{
    public class ProductWithBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandSpecification(int productId)
            : base(p => p.Id.Equals(productId))
        {
            AddInclude(p => p.Brand);
        }

        public ProductWithBrandSpecification(string slug)
            : base(p => p.Slug.ToLower().Equals(slug.ToLower()))
        {
            AddInclude(p => p.Brand);
        }
    }
}
