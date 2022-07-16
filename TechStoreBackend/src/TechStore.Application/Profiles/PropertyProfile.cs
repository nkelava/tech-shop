using AutoMapper;
using TechStore.Application.Models.Property;
using TechStore.Domain.Entities;


namespace TechStore.Application.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyReadModel>().ReverseMap();
            CreateMap<Property, PropertyCreateModel>().ReverseMap();
            CreateMap<Property, PropertyUpdateModel>().ReverseMap();
        }
    }
}
