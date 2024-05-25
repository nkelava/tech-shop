using AutoMapper;
using TechStore.Application.Models.AttributeValue;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Profiles
{
    public class AttributeValueProfile : Profile
    {
        public AttributeValueProfile()
        {
            CreateMap<ProductAttributeValue, AttributeValueCreateModel>().ReverseMap();
            CreateMap<ProductAttributeValue, AttributeValueUpdateModel>().ReverseMap();
            CreateMap<ProductAttributeValue, AttributeValueReadModel>().ReverseMap();
        }
    }
}
