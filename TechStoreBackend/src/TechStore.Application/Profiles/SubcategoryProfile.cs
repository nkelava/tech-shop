using AutoMapper;
using TechStore.Application.Models.Subcategory;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Profiles
{
    public class SubcategoryProfile : Profile
    {
        public SubcategoryProfile()
        {
           CreateMap<Subcategory, SubcategoryReadModel>().ReverseMap();    
           CreateMap<Subcategory, SubcategoryCreateModel>().ReverseMap();    
           CreateMap<Subcategory, SubcategoryUpdateModel>().ReverseMap();    
        }
    }
}
