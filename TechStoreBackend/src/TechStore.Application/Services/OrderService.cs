using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Order;
using TechStore.Application.Specifications.OrderSpecification;
using TechStore.Domain.Entities.OrderAggregate;
using TechStore.Domain.Enums.Order;


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
            var order = _mapper.Map<Order>(orderModel);
            order.Products.Clear();

            foreach (var orderProduct in orderModel.Products)
            {
                var product = _repository.Product.FindById(orderProduct.Product.Id);
                order.Products.Add(new OrderProduct{
                    Quantity = orderProduct.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = orderProduct.Quantity * product.Price,
                    ProductId = product.Id,
                    Product = product
                });
            }

            order.TotalPrice = order.CalculateTotalPrice();

            _repository.Order.Add(order);
            await _repository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int orderId)
        {
            var order = await _repository.Order.GetOrderByIdAsync(orderId);

            if (order is null)
                return 0;

            _repository.Order.Delete(order);
            await _repository.SaveAsync();
            
            return order.Id;
        }

        public async Task UpdateAsync(OrderUpdateModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);
            order.UpdatedAt = DateTime.Now;

            _repository.Order.Update(order);
            await _repository.SaveAsync();
        }

        public async Task<int> UpdateOrderStatusAsync(OrderUpdateStatusModel updateStatusModel)
        {
            var order = _repository.Order.FindById(updateStatusModel.OrderId);

            if (order == null)
                return 0;

            var status = (OrderStatus)updateStatusModel.OrderStatusValue;
            order.Status = status;
            order.UpdatedAt = DateTime.Now;

            _repository.Order.Update(order);
            await _repository.SaveAsync();

            return order.Id;
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
            var spec = new OrderWithProductsSpecification(email);
            var orders = await _repository.Order.Find(spec).ToListAsync();

            foreach (var order in orders) {
                foreach(var product in order.Products)
                {
                    product.Product = await _repository.Product.GetProductByIdAsync(product.ProductId);
                }
            }

            var ordersModel = _mapper.Map<IList<OrderReadModel>>(orders);

            return ordersModel;
        }
    }
}
