using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Cart;
using TechStore.Application.Specifications.CartSpecification;
using TechStore.Domain.Entities.Cart;

namespace TechStore.Application.Services
{
    public class CartService : ICartService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CartService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddProduct(string username, int productId)
        {
            var cart = await GetExistingOrCreateNewCart(username);
            //var productUnitPrice = await _repository.Product.GetProductPrice(productId);
            //var product = await _repository.Product.GetByIdAsync(productId);

            //cart.AddProduct(productId, productUnitPrice);
            //cart.AddProduct(productId, unitPrice: product.UnitPrice);
            cart.AddProduct(productId, unitPrice: 10);

            _repository.Cart.Update(cart);
            await _repository.SaveAsync();
        }


        public async Task RemoveProduct(int cartId, int productId)
        {
            var spec = new CartWithProductsSpecification(cartId);
            var cart = (await _repository.Cart.Find(spec)).FirstOrDefault();

            if (cart == null)
                return;

            cart.RemoveProduct(productId);

            _repository.Cart.Update(cart);
            await _repository.SaveAsync();
        }


        public async Task ClearCart(string username)
        {
            var cart = await _repository.Cart.GetByUsernameAsync(username);

            if (cart == null)
                return;

            cart.Clear();

            _repository.Cart.Update(cart);
            await _repository.SaveAsync();
        }


        public async Task<CartReadModel> GetByUsername(string username)
        {
            var cart = await GetExistingOrCreateNewCart(username);
            var cartModel = _mapper.Map<CartReadModel>(cart);

            // If movie can't be loaded from razor page, we than manual map it
            if (cart.Products.Any(c => c.Product == null))
            {
                cartModel.CartProducts.Clear();

                foreach (var item in cart.Products)
                {
                    var cartProductModel = _mapper.Map<CartProductModel>(item);
                    var product = await _repository.Product.GetProductWithSubcategoryByIdAsync(item.ProductId);
                    var productModel = _mapper.Map<ProductReadModel>(product);
                    cartProductModel.Product = productModel;
                    cartModel.CartProducts.Add(cartProductModel);
                }
            }

            return cartModel;
        }


        private async Task<Cart> GetExistingOrCreateNewCart(string username)
        {
            var cart = await _repository.Cart.GetByUsernameAsync(username);

            if (cart != null)
                return cart;

            // If it's first time create new cart
            var newCart = new Cart
            {
                Username = username
            };

            _repository.Cart.Create(newCart);
            await _repository.SaveAsync();

            return newCart;
        }
    }
}
