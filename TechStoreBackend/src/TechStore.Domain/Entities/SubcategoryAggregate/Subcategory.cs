using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Domain.Entities.SubcategoryAggregate
{
    public class Subcategory : Entity
    {
        public string Name { get; set; }
        //public int Sort { get; set; }


        // n - 1
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        // n - n 
        public List<SubcategoryProperty> Properties { get; set; }
    }
}
