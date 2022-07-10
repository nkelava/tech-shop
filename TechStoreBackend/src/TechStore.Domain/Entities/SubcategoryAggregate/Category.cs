using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.SubcategoryAggregate
{
    public class Category : Entity
    {
        public string Name { get; set; }
        //public int Sort { get; set; }

        // 1 - n
        public List<Subcategory> Subcategories { get; set; }
    }
}
