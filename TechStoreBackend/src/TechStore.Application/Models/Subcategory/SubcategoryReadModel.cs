using TechStore.Application.Models.Base;
using TechStore.Application.Models.Category;


namespace TechStore.Application.Models.Subcategory
{
    public class SubcategoryReadModel: BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? ImageURL { get; set; }

        public CategoryReadModel Category { get; set; }
    }
}
