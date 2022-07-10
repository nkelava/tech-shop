using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Specifications.ProductSpecification
{
    public class ProductsWithBrandSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandSpecification()
            : base()
        {
            AddInclude(p => p.Brand);
        }

        public ProductsWithBrandSpecification(int brandId)
            : base(p => p.Brand.Id.Equals(brandId))
        {
            AddInclude(p => p.Brand);
        }

        public ProductsWithBrandSpecification(string brandName)
            : base(p => p.Brand.Name.ToLower().Equals(brandName.ToLower()))
        {
            AddInclude(p => p.Brand);
        }
    }
}
