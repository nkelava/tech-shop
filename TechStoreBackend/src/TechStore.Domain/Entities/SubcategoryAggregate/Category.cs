using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.SubcategoryAggregate
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // 1 - n
        public List<Subcategory> Subcategories { get; set; }
    }
}
