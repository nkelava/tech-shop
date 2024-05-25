

namespace TechStore.Application.Models.Subcategory
{
    public class SubcategoryCreateModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? ImageURL { get; set; } = null;
        public int CategoryId { get; set; }
    }
}
