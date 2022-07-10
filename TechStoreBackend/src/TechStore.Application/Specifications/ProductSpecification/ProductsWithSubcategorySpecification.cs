

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
        }

        public ProductsWithSubcategorySpecification(int subcategoryId)
            : base(p => p.Subcategory.Id.Equals(subcategoryId))
        {
            AddInclude(p => p.Subcategory);
        }

        public ProductsWithSubcategorySpecification(string subcategoryName)
            : base(p => p.Subcategory.Name.ToLower().Equals(subcategoryName.ToLower()))
        {
            AddInclude(p => p.Subcategory);
        }
    }
}
