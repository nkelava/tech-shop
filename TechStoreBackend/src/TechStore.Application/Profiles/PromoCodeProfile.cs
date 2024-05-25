using AutoMapper;
using TechStore.Application.Models.PromoCode;
using TechStore.Domain.Entities;


namespace TechStore.Application.Profiles
{
    public class PromoCodeProfile : Profile
    {
        public PromoCodeProfile()
        {
            CreateMap<PromoCode, PromoCodeCreateModel>().ReverseMap();
            CreateMap<PromoCode, PromoCodeReadModel>().ReverseMap();
            CreateMap<PromoCode, PromoCodeUpdateModel>().ReverseMap();
        }
    }
}
