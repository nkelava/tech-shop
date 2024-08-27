using AutoMapper;
using TechStore.Application.Models.Wishlist;
using TechStore.Domain.Entities.WishlistAggregate;


namespace TechStore.Application.Profiles
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {
            CreateMap<Wishlist, WishlistReadModel>().ReverseMap();
            CreateMap<WishlistProduct, WishlistProductModel>().ReverseMap();
        }
    }
}
