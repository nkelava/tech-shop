using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.ProductAggregate
{
    public class Review : Entity
    {
        public string Email { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public bool IsReported { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        // n - 1
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
