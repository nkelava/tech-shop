using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Specifications.ProductSpecification
{
    public class ProductWithSubcategorySpecification : BaseSpecification<Product>
    {
        public ProductWithSubcategorySpecification()
            : base()
        {

        }

        public ProductWithSubcategorySpecification(int productId)
            : base(p => p.Id.Equals(productId))
        {
            AddInclude(p => p.Subcategory);
        }

        public ProductWithSubcategorySpecification(string slug)
           : base(p => p.Slug.ToLower().Equals(slug.ToLower()))
        {
            AddInclude(p => p.Subcategory);
        }
    }
}
