

namespace TechStore.Application.Models.Subcategory
{
    public class SubcategoryCreateModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
    }
}
