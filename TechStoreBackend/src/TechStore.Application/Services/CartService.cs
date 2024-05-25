using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Cart;
using TechStore.Application.Models.Product;
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


        public async Task AddProductAsync(string email, int productId, int quantity)
        {
            var cart = await GetExistingOrCreateNewCart(email);
            var product = await _repository.Product.GetProductByIdAsync(productId);

            if (product is not null)
            {
                cart.AddProduct(product, quantity, unitPrice: product.Price);

                _repository.Cart.Update(cart);
                await _repository.SaveAsync();
            }
        }


        public async Task RemoveProductAsync(int cartId, int productId)
        {
            var spec = new CartWithProductsSpecification(cartId);
            var cart = _repository.Cart.Find(spec).FirstOrDefault();

            if (cart is null)
                return;

            cart.RemoveProduct(productId);

            _repository.Cart.Update(cart);
            await _repository.SaveAsync();
        }


        public async Task<int> ClearCart(string email)
        {
            var cart = await _repository.Cart.GetByEmailAsync(email);

            if (cart is null)
                return 0;

            cart.Clear();

            _repository.Cart.Update(cart);
            await _repository.SaveAsync();
            return cart.Id;
        }


        public async Task<CartReadModel> GetByEmailAsync(string email)
        {
            var cart = await GetExistingOrCreateNewCart(email);
            var cartModel = _mapper.Map<CartReadModel>(cart);

            // If product can't be loaded from page we than manual map it
            if (cart.Products.Any(c => c.Product == null))
            {
                cartModel.Products.Clear();

                foreach (var item in cart.Products)
                {
                    var cartProductModel = _mapper.Map<CartProductModel>(item);
                    var product = await _repository.Product.GetProductByIdAsync(item.ProductId);
                    var productModel = _mapper.Map<ProductReadModel>(product);
                    cartProductModel.Product = productModel;
                    cartModel.Products.Add(cartProductModel);
                }
            }

            return cartModel;
        }


        private async Task<Cart> GetExistingOrCreateNewCart(string email)
        {
            var cart = await _repository.Cart.GetByEmailAsync(email);

            if (cart is not null)
                return cart;

            // If it's first time create new cart
            var newCart = new Cart {
                Email = email
            };

            _repository.Cart.Add(newCart);
            await _repository.SaveAsync();

            return newCart;
        }
    }
}
