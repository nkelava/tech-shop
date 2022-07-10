using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Specifications.CategorySpecification
{
    public class CategoryWithSubcategorySpecification : BaseSpecification<Category>
    {
        public CategoryWithSubcategorySpecification(int categoryId)
            : base(c => c.Id.Equals(categoryId))
        {
            AddInclude(c => c.Subcategories);
        }
    }
}
