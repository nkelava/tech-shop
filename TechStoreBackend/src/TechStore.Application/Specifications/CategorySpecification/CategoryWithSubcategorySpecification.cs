using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Specifications.CategorySpecification
{
    public class CategoryWithSubcategorySpecification : BaseSpecification<Category>
    {
        public CategoryWithSubcategorySpecification(int id)
            : base(c => c.Id.Equals(id))
        {
            AddInclude(c => c.Subcategories);
        }

        public CategoryWithSubcategorySpecification(string slug)
           : base(c => c.Slug.ToLower().Equals(slug.ToLower()))
        {
            AddInclude(c => c.Subcategories);
        }
    }
}
