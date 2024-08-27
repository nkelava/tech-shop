using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Product;
using TechStore.Application.Models.Wishlist;
using TechStore.Application.Specifications.WishlistSpecification;
using TechStore.Domain.Entities.User;
using TechStore.Domain.Entities.WishlistAggregate;


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


        public async Task<WishlistReadModel?> AddProductAsync(ApplicationUser user, int productId)
        {
            var wishlist = await GetExistingOrCreateNewWishlist(user);
            var product = await _repository.Product.GetByIdAsync(productId);

            if (product == null)
                return null;

            wishlist.AddProduct(product);

            _repository.Wishlist.Update(wishlist);
            await _repository.SaveAsync();

            var wishlistModel = _mapper.Map<WishlistReadModel>(wishlist);
            return wishlistModel;
        }

        public async Task<WishlistReadModel?> RemoveProductAsync(int wishlistId, int productId)
        {
            var spec = new WishlistWithProductsSpecification(wishlistId);
            var wishlist = _repository.Wishlist.Find(spec).FirstOrDefault();

            if (wishlist == null)
                return null;

            var removedWishlistId = wishlist.RemoveProduct(productId);

            if (removedWishlistId == null)
                return null;

            _repository.Wishlist.Update(wishlist);
            await _repository.SaveAsync();

            var wishlistModel = _mapper.Map<WishlistReadModel>(wishlist);
            return wishlistModel;
        }

        public async Task<WishlistReadModel> GetAsync(ApplicationUser user)
        {
            var wishlist = await GetExistingOrCreateNewWishlist(user);
            var wishlistModel = _mapper.Map<WishlistReadModel>(wishlist);

            // If product can't be loaded from page we than manually map it
            foreach (var item in wishlist.Products) {
                if (item.Product is null)
                {
                    var wishlistProductModel = _mapper.Map<WishlistProductModel>(item);
                    var product = await _repository.Product.GetByIdAsync(item.ProductId);
                    var productModel = _mapper.Map<ProductReadModel>(product);
                    wishlistProductModel.Product = productModel;
                    wishlistModel.Products.Add(wishlistProductModel);
                }
            }

            return wishlistModel;
        }

        private async Task<Wishlist> GetExistingOrCreateNewWishlist(ApplicationUser user)
        {
            var wishlist = await _repository.Wishlist.GetByUserIdAsync(user.Id);

            if (wishlist != null)
                return wishlist;

            // Create new in case of first attempt
            var newWishlist = new Wishlist {
                ApplicationUserId = user.Id
            };

            _repository.Wishlist.Add(newWishlist);
            await _repository.SaveAsync();

            return newWishlist;
        }
    }
}
