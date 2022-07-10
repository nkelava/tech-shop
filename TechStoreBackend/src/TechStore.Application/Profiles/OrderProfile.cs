using AutoMapper;
using TechStore.Application.Models.Order;
using TechStore.Domain.Entities.Order;


namespace TechStore.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderCreateModel>().ReverseMap();
            CreateMap<Order, OrderReadModel>().ReverseMap();
            CreateMap<Order, OrderUpdateModel>().ReverseMap();
            CreateMap<OrderProduct, OrderProductModel>().ReverseMap();
        }
    }
}
