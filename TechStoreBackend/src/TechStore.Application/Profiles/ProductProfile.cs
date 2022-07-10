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
            CreateMap<Product, ProductCreateModel>().ReverseMap();
            CreateMap<Product, ProductUpdateModel>().ReverseMap();
            CreateMap<ProductProperty, ProductPropertyModel>().ReverseMap();
        }
    }
}
