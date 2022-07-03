using AutoMapper;
using TechStore.Application.Models.Category;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryCreateModel>().ReverseMap();
            CreateMap<Category, CategoryReadModel>().ReverseMap();
            CreateMap<Category, CategoryUpdateModel>().ReverseMap();
        }
    }
}
