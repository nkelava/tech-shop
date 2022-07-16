using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.SubcategoryAggregate
{
    public class SubcategoryProperty
    {
        public int SubcategoryId { get; set; }
        public  Subcategory Subcategory { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
