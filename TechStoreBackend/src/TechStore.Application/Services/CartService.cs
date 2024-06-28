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


        public async Task<CartReadModel?> AddProductAsync(string email, CartCreateModel createModel)
        {
            var cart = await GetExistingOrCreateNewCart(email);
            var product = await _repository.Product.GetByIdAsync(createModel.ProductId);

            if (product == null)
                return null;

            cart.AddProduct(product, createModel.Quantity, unitPrice: product.Price);
            
            _repository.Cart.Update(cart);
            await _repository.SaveAsync();

            var cartModel = _mapper.Map<CartReadModel>(cart);
            return cartModel;
        }

        public async Task<CartReadModel?> RemoveProductAsync(int cartId, int productId)
        {
            var spec = new CartWithProductsSpecification(cartId);
            var cart = _repository.Cart.Find(spec).FirstOrDefault();
            
            if (cart == null)
                return null;

            var removedProductId = cart.RemoveProduct(productId);

            if (removedProductId == null)
                return null;

            _repository.Cart.Update(cart);
            await _repository.SaveAsync();

            var cartModel = _mapper.Map<CartReadModel>(cart);
            return cartModel;
        }

        public async Task<CartReadModel?> ClearCart(string email)
        {
            var cart = await _repository.Cart.GetByEmailAsync(email);

            if (cart == null)
                return null;

            cart.Clear();

            _repository.Cart.Update(cart);
            await _repository.SaveAsync();

            var cartModel = _mapper.Map<CartReadModel>(cart);
            return cartModel;
        }

        public async Task<CartReadModel> GetByEmailAsync(string email)
        {
            var cart = await GetExistingOrCreateNewCart(email);
            var cartModel = _mapper.Map<CartReadModel>(cart);

            // If product can't be loaded from page we than manually map it
            if (cart.Products.Any(c => c.Product == null))
            {
                cartModel.Products.Clear();

                foreach (var item in cart.Products)
                {
                    var cartProductModel = _mapper.Map<CartProductModel>(item);
                    var product = await _repository.Product.GetByIdAsync(item.ProductId);
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

            if (cart != null)
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
