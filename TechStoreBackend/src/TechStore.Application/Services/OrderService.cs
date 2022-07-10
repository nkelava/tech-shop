using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Order;
using TechStore.Domain.Entities.Order;


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

        public async Task AddAsync(OrderCreateModel orderModel)
        {
            ValidateOrder(orderModel);

            var order = _mapper.Map<Order>(orderModel);

            _repository.Order.Add(order);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int orderId)
        {
            var order = _repository.Order.GetOrderById(orderId);

            _repository.Order.Delete(order);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(OrderUpdateModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);

            _repository.Order.Update(order);
            await _repository.SaveAsync();
        }

        public OrderReadModel GetOrderById(int orderId)
        {
            var order = _repository.Order.GetOrderById(orderId);
            var orderModel = _mapper.Map<OrderReadModel>(order);

            return orderModel;
        }

        public OrderReadModel GetOrderByEmail(string email)
        {
            var order = _repository.Order.GetOrderByEmail(email);
            var orderModel = _mapper.Map<OrderReadModel>(order);

            return orderModel;
        }

        public IList<OrderReadModel> GetOrders()
        {
            var orders = _repository.Order.GetAllOrders();
            var ordersModel = _mapper.Map<IList<OrderReadModel>>(orders);

            return ordersModel;
        }

        private void ValidateOrder(OrderCreateModel orderModel)
        {
            if (string.IsNullOrWhiteSpace(orderModel.Email))
                throw new ApplicationException("Order username must be defined. Can not be empty or white space!!!");

            if (orderModel.Products.Count == 0)
                throw new ApplicationException("Order must contain at least one item.");
        }
    }
}
