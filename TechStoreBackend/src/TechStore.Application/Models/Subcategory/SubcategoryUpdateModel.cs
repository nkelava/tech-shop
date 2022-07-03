using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Subcategory
{
    public class SubcategoryUpdateModel : ModelBase
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}
