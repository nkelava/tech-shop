using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    public class PromoCode : Entity
    {
        public string Code { get; set; }
        public int Discount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
