using TechStore.Application.Models.Product;


namespace TechStore.Application.Models.Review
{
    public  class ReviewCreateModel
    {
        public string Email { get; set; }

        public decimal Rate { get; set; } = 0;

        public string Comment { get; set; }

        public int ProductId { get; set; }
    }
}
