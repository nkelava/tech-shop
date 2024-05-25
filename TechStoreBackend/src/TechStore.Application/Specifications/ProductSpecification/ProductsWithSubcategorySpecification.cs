

using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.ProductAggregate;

namespace TechStore.Application.Specifications.ProductSpecification
{
    public class ProductsWithSubcategorySpecification : BaseSpecification<Product>
    {
        public ProductsWithSubcategorySpecification()
            : base()
        {
            AddInclude(p => p.Subcategory);
            AddInclude(p => p.Subcategory.Category);
        }

        public ProductsWithSubcategorySpecification(int subcategoryId)
            : base(p => p.Subcategory.Id.Equals(subcategoryId))
        {
            AddInclude(p => p.Subcategory);
            AddInclude(p => p.Subcategory.Category);
        }

        public ProductsWithSubcategorySpecification(string subcategorySlug)
            : base(p => p.Subcategory.Slug.ToLower().Equals(subcategorySlug.ToLower()))
        {
            AddInclude(p => p.Subcategory);
            AddInclude(p => p.Subcategory.Category);
        }
    }
}
