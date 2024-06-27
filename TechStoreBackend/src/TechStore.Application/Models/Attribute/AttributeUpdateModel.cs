using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Attribute
{
    public class AttributeUpdateModel
    {
        [Required(ErrorMessage = "Attribute name is required.")]
        public string Name { get; set; }
    }
}
