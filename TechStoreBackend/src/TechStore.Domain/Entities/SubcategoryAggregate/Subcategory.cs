using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Domain.Entities.SubcategoryAggregate
{
    public class Subcategory : Entity
    {
        public string Name { get; set; }
        //public int Sort { get; set; }


        // n - 1
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        // 1 - n
        public IList<Product> Products { get; set; }


        // n - n 
        public IList<SubcategoryProperty> Properties { get; set; }
    }
}
