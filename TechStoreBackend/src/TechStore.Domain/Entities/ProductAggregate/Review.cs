using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.ProductAggregate
{
    public class Review : Entity
    {
        public string Email { get; set; }
        public decimal Rate { get; set; } = 0;
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        // n - 1
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
