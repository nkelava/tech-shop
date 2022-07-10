using AutoMapper;
using TechStore.Application.Models.Review;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewCreateModel>().ReverseMap();
            CreateMap<Review, ReviewReadModel>().ReverseMap();
        }
    }
}
