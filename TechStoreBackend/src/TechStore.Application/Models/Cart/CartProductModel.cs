using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Cart
{
    public class CartProductModel : BaseModel
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }
        //public ProductReadModel Product { get; set; }
    }
}
