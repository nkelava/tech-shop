using TechStore.Application.Models.Base;
using TechStore.Application.Models.Product;


namespace TechStore.Application.Models.Cart
{
    public class CartProductModel : BaseModel
    {
        public int Quantity { get; set; }
        public ProductReadModel Product { get; set; }
    }
}
