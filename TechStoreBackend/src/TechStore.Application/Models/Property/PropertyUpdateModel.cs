using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Property
{
    public class PropertyUpdateModel : BaseModel
    {
        public string Name { get; set; }
        public string ValueType { get; set; }
    }
}
