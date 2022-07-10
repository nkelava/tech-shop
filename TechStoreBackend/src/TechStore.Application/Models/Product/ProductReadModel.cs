using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Product
{
    public class ProductReadModel : BaseModel
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; } = 0;

        public decimal Rating { get; set; } = 0;

    }
}
