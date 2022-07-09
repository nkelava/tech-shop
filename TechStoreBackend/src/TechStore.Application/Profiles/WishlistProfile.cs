using AutoMapper;
using TechStore.Application.Models.Wishlist;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Application.Profiles
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {
            CreateMap<Wishlist, WishlistReadModel>().ReverseMap();
        }
    }
}
