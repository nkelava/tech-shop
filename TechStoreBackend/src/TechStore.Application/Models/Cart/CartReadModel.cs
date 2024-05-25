using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Cart
{
    public class CartReadModel : BaseModel
    {
        public List<CartProductModel> Products { get; set; } = new List<CartProductModel>();
    }
}
