using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.PromoCode
{
    public class PromoCodeReadModel : BaseModel
    {
        public string Code { get; set; }
        public int Discount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
