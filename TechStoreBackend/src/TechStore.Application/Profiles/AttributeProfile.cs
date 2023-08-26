using AutoMapper;
using TechStore.Application.Models.Attribute;
using TechStore.Domain.Entities.ProductAggregate;

namespace TechStore.Application.Profiles
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile()
        {
            CreateMap<ProductAttribute, AttributeReadModel>().ReverseMap();
            CreateMap<ProductAttribute, AttributeCreateModel>().ReverseMap();
            CreateMap<ProductAttribute, AttributeUpdateModel>().ReverseMap();
            CreateMap<ProductAttributeValue, AttributeValueReadModel>().ReverseMap();
        }
    }
}
