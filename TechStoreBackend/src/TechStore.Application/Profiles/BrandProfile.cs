using AutoMapper;
using TechStore.Application.Models.Brand;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandCreateModel>().ReverseMap();
            CreateMap<Brand, BrandUpdateModel>().ReverseMap();
        }
    }
}
