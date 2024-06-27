using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.AttributeValue
{
    public class AttributeValueUpdateModel
    {
        [Required(ErrorMessage = "Please provide a value.")]
        public string Value { get; set; }
    }
}
