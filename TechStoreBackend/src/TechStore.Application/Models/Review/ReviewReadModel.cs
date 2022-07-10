

namespace TechStore.Application.Models.Review
{
    public class ReviewReadModel
    {
        public string Email { get; set; }

        public decimal Rate { get; set; } = 0;

        public string Comment { get; set; }
    }
}
