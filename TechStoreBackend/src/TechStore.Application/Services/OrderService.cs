using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Order;
using TechStore.Domain.Entities.OrderAggregate;


namespace TechStore.Application.Services
{
    public class OrderService : IOrderService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(OrderCreateModel orderModel)
        {
            ValidateOrder(orderModel);

            var order = _mapper.Map<Order>(orderModel);

            _repository.Order.Add(order);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int orderId)
        {
            var order = await _repository.Order.GetOrderByIdAsync(orderId);

            _repository.Order.Delete(order);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(OrderUpdateModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);

            _repository.Order.Update(order);
            await _repository.SaveAsync();
        }

        public async Task<OrderReadModel> GetOrderByIdAsync(int orderId)
        {
            var order = await _repository.Order.GetOrderByIdAsync(orderId);
            var orderModel = _mapper.Map<OrderReadModel>(order);

            return orderModel;
        }

        public async Task<IEnumerable<OrderReadModel>> GetOrdersAsync()
        {
            var orders = await _repository.Order.GetAllOrdersAsync();
            var ordersModel = _mapper.Map<IList<OrderReadModel>>(orders);

            return ordersModel;
        }

        public async Task<IEnumerable<OrderReadModel>> GetOrdersAsync(string email)
        {
            var orders = await _repository.Order.GetAllOrdersAsync(email);
            var ordersModel = _mapper.Map<IList<OrderReadModel>>(orders);

            return ordersModel;
        }

        private static void ValidateOrder(OrderCreateModel orderModel)
        {
            if (string.IsNullOrWhiteSpace(orderModel.Email))
                throw new ApplicationException("Order username must be defined. Can not be empty or white space!!!");

            if (orderModel.Products.Count == 0)
                throw new ApplicationException("Order must contain at least one item.");
        }
    }
}
