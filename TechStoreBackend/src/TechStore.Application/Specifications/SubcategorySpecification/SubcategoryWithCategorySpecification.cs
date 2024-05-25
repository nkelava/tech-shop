using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Specifications.SubcategorySpecification
{
    public class SubcategoryWithCategorySpecification:  BaseSpecification<Subcategory>
    {
        public SubcategoryWithCategorySpecification(int subcategoryId)
        : base(s => s.Id.Equals(subcategoryId))
        {
            AddInclude(s => s.Category);
        }
    }
}
