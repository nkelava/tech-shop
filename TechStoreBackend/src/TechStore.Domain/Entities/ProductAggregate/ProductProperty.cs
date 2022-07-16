using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.ProductAggregate
{
    public class ProductProperty
    {
        public string Value { get; set; }


        // n - n 
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
