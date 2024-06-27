using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Attribute
{
    public class AttributeCreateModel
    {
        [Required(ErrorMessage = "Please provide a name.")]
        [MaxLength(28, ErrorMessage = "Name must be 28 characters or fewer.")]
        public string Name { get; set; }
    }
}
