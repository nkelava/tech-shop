using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Cart
{
    public class CartReadModel : BaseModel
    {
        public string Username { get; set; }
        public decimal TotalPrice { get; set; }

        public List<CartProductModel> CartProducts { get; set; } = new List<CartProductModel>();
    }
}
