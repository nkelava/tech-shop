using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.ProductAggregate
{
    public class Brand : Entity
    {
        public string Name { get; set; }


        // 1 - n
        public IList<Product> Products { get; set; }
    }
}
