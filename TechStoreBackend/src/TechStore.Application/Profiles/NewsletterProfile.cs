using AutoMapper;
using TechStore.Application.Models.Newsletter;
using TechStore.Domain.Entities;


namespace TechStore.Application.Profiles
{
    public class NewsletterProfile : Profile
    {
        public NewsletterProfile()
        {
            CreateMap<Newsletter, NewsletterReadModel>().ReverseMap();
            CreateMap<Newsletter, NewsletterCreateModel>().ReverseMap();
        }
    }
}
