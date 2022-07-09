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

        public async Task AddItem(string username, int productId)
        {
            var wishlist = await GetExistingOrCreateNewWishlist(username);
            wishlist.AddProduct(productId);

            _repository.Wishlist.Update(wishlist);
            await _repository.SaveAsync();
        }

        public async Task RemoveItem(int wishlistId, int productId)
        {
            var spec = new WishlistWithProductsSpecification(wishlistId);
            var wishlist = (await _repository.Wishlist.Find(spec)).FirstOrDefault();

            if (wishlist == null) return;

            wishlist.RemoveProduct(productId);

            _repository.Wishlist.Update(wishlist);
            await _repository.SaveAsync();
        }

        public async Task<WishlistReadModel> GetWishlistByUsernameAsync(string username)
        {
            var wishlist = await GetExistingOrCreateNewWishlist(username);
            var wishlistReadModel = _mapper.Map<WishlistReadModel>(wishlist);

            if (wishlist.Products != null)
            {
                foreach (var item in wishlist.Products)
                {
                    //var product = await _repository.Product.GetProductByIdAsync(item.ProductId);
                    //var productReadModel = _mapper.Map<ProductReadModel>(product);
                    var productReadModel = new ProductReadModel { Id = 4, Name = "Test" };
                    wishlistReadModel.WishlistProducts.Add(productReadModel);

                }
            }

            return wishlistReadModel;
        }

        private async Task<Wishlist> GetExistingOrCreateNewWishlist(string username)
        {
            var wishlist = await _repository.Wishlist.GetWishlistByUsernameAsync(username);

            if (wishlist != null)
                return wishlist;

            // Create new in case of first attempt
            var newWishlist = new Wishlist
            {
                Username = username
            };

            _repository.Wishlist.Create(newWishlist);
            await _repository.SaveAsync();

            return newWishlist;
        }
    }
}
