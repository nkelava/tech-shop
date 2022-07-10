using AutoMapper;
using TechStore.Application.Models.Cart;
using TechStore.Domain.Entities.Cart;


namespace TechStore.Application.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartReadModel>().ReverseMap();
            CreateMap<CartProduct, CartProductModel>().ReverseMap();
        }
    }
}
