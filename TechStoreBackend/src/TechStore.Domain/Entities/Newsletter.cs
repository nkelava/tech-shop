using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities
{
    public class Newsletter : Entity
    {
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
