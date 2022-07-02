using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Domain.Entities
{
    public class Property : Entity
    {
        public string Name { get; set; }
        public string ValueType { get; set; }
        //public int Sort { get; set; }


        // n - n
        public IList<ProductProperty> Products { get; set; }
        public IList<SubcategoryProperty> Subcategories { get; set; }
    }
}
