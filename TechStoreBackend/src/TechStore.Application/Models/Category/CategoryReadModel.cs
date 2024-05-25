using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Category
{
    public class CategoryReadModel: BaseModel
    {
        public string Name { get; set; }

        public string Slug { get; set; }
    }
}
