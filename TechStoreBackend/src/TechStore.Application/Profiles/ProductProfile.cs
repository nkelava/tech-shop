using AutoMapper;
using TechStore.Application.Models.Product;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadModel>().ReverseMap();
        }
    }
}
