using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Review
{
    public class ReviewReadModel : BaseModel
    {
        public string Email { get; set; }

        public decimal Rate { get; set; } = 0;

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
