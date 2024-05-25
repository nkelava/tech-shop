using TechStore.Application.Models.Subcategory;


namespace TechStore.Application.Models.Category
{
    public class CategoryWithSubcategoriesModel
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public IList<SubcategoryReadModel> Subcategories { get; set; }
    }
}
