namespace TechStore.Domain.Entities.ProductAggregate
{
    public class ProductAttributeSet
    {
        // n - n 
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int AttributeId { get; set; }
        public ProductAttribute Attribute { get; set; }

        public int AttributeValueId { get; set; }
        public ProductAttributeValue AttributeValue { get; set; }
    }
}
