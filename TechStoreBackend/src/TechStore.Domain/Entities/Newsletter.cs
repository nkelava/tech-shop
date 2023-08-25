using System.ComponentModel.DataAnnotations;


namespace TechStore.Domain.Entities
{
    public class Newsletter
    {
        [Key]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
