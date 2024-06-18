using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    public class PromoCode : Entity
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }

        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
