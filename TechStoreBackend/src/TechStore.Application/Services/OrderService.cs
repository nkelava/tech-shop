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


        public async Task CreateAsync(OrderCreateModel createModel)
        {
            if (createModel == null)
                throw new ArgumentNullException(nameof(createModel), "Order model cannot be null.");

            var order = _mapper.Map<Order>(createModel);
            order.Products.Clear();

            foreach (var orderProduct in createModel.Products)
            {
                var product = _repository.Product.FindById(orderProduct.Product.Id);

                if (product == null)
                    throw new InvalidOperationException($"Product with ID {orderProduct.Product.Id} not found.");

                if (product.UnitsInStock < orderProduct.Quantity)
                    throw new InvalidOperationException("Insufficient stock available for the requested products.");

                var newOrderProduct = new OrderProduct
                {
                    Quantity = orderProduct.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = orderProduct.Quantity * product.Price,
                    ProductId = product.Id,
                    Product = product
                };

                order.Products.Add(newOrderProduct);
            }

            order.TotalPrice = order.CalculateTotalPrice();

            _repository.Order.Add(order);
            await _repository.SaveAsync();

            foreach (var orderProduct in createModel.Products)
            {
                var product = _repository.Product.FindById(orderProduct.Product.Id);

                product.UnitsInStock -= orderProduct.Quantity;
                _repository.Product.Update(product);
            }

            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(OrderUpdateModel updateModel)
        {
            var order = _mapper.Map<Order>(updateModel);
            order.UpdatedAt = DateTime.Now;

            _repository.Order.Update(order);
            await _repository.SaveAsync();
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _repository.Order.GetByIdAsync(id);

            if (order == null)
                return false;

            _repository.Order.Delete(order);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<int?> UpdateOrderStatusAsync(OrderUpdateStatusModel updateModel)
        {
            var order = _repository.Order.FindById(updateModel.OrderId);

            if (order == null)
                return null;

            order.Status = (OrderStatus)updateModel.OrderStatusValue;
            order.UpdatedAt = DateTime.Now;

            _repository.Order.Update(order);
            await _repository.SaveAsync();

            return order.Id;
        }

        public async Task<int?> UpdatePaymentStatusAsync(int orderId, PaymentStatus paymentStatus)
        {
            var order = _repository.Order.FindById(orderId);

            if (order == null)
                return null;

            order.PaymentStatus = (PaymentStatus)paymentStatus;
            order.UpdatedAt = DateTime.Now;

            _repository.Order.Update(order);
            await _repository.SaveAsync();

            return order.Id;
        }

        public async Task<OrderReadModel?> GetByIdAsync(int id)
        {
            var order = await _repository.Order.GetByIdAsync(id);

            if (order == null)
                return null;

            var orderModel = _mapper.Map<OrderReadModel>(order);
            return orderModel;
        }

        public async Task<OrderReadModel?> GetBySessionIdAsync(string sessionId)
        {
            var order = await _repository.Order.GetBySessionIdAsync(sessionId);

            if (order == null)
                return null;

            var orderModel = _mapper.Map<OrderReadModel>(order);
            return orderModel;
        }

        public async Task<IEnumerable<OrderReadModel>> GetAllAsync()
        {
            var orders = await _repository.Order.GetAllAsync();
            var ordersModel = _mapper.Map<IList<OrderReadModel>>(orders);

            return ordersModel;
        }

        public async Task<IEnumerable<OrderReadModel>> GetAllAsync(string userId)
        {
            var spec = new OrderWithProductsSpecification(userId);
            var orders = await _repository.Order.Find(spec).ToListAsync();

            if (orders != null)
            {
                foreach (var order in orders) {
                    foreach(var product in order.Products)
                    {
                        var foundProduct = await _repository.Product.GetByIdAsync(product.ProductId);

                        if (foundProduct != null)
                        {
                            product.Product = foundProduct;
                        }
                    }
                }
            }

            var ordersModel = _mapper.Map<IList<OrderReadModel>>(orders);
            return ordersModel;
        }
    }
}
