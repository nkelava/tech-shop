using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Product;
using TechStore.Application.Models.Wishlist;
using TechStore.Application.Specifications.WishlistSpecification;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Application.Services
{
    public class WishlistService : IWishlistService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public WishlistService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddProductAsync(string email, int productId)
        {
            var wishlist = await GetExistingOrCreateNewWishlist(email);
            var product = await _repository.Product.GetProductByIdAsync(productId);

            if (product is not null)
            {
                wishlist.AddProduct(product);

                _repository.Wishlist.Update(wishlist);
                await _repository.SaveAsync();
            }
        }


        public async Task<int> RemoveProductAsync(int wishlistId, int productId)
        {
            var spec = new WishlistWithProductsSpecification(wishlistId);
            var wishlist = _repository.Wishlist.Find(spec).FirstOrDefault();

            if (wishlist is null)
                return 0;

            wishlist.RemoveProduct(productId);

            _repository.Wishlist.Update(wishlist);
            await _repository.SaveAsync();
            return productId;
        }


        public async Task<WishlistReadModel> GetByEmailAsync(string email)
        {
            var wishlist = await GetExistingOrCreateNewWishlist(email);
            var wishlistModel = _mapper.Map<WishlistReadModel>(wishlist);

            foreach (var item in wishlist.Products) {
                if (item.Product is null)
                {
                    var wishlistProductModel = _mapper.Map<WishlistProductModel>(item);
                    var product = await _repository.Product.GetProductByIdAsync(item.ProductId);
                    var productModel = _mapper.Map<ProductReadModel>(product);
                    wishlistProductModel.Product = productModel;
                    wishlistModel.Products.Add(wishlistProductModel);
                }
            }

            return wishlistModel;
        }


        private async Task<Wishlist> GetExistingOrCreateNewWishlist(string email)
        {
            var wishlist = _repository.Wishlist.GetByEmailAsync(email);

            if (wishlist is not null)
                return wishlist;

            // Create new in case of first attempt
            var newWishlist = new Wishlist {
                Email = email
            };

            _repository.Wishlist.Add(newWishlist);
            await _repository.SaveAsync();

            return newWishlist;
        }
    }
}
