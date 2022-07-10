using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Infrastructure.Data;


namespace TechStore.Infrastructure.Repositories.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TechStoreContext _repositoryContext;
        private IBrandRepository _brand;
        private ICartRepository _cart;
        private ICategoryRepository _category;
        private IProductRepository _product;
        private ISubcategoryRepository _subcategory;
        private IWishlistRepository _wishlist;

        public IBrandRepository Brand
        {
            get
            {
                if (_brand == null)
                {
                    _brand = new BrandRepository(_repositoryContext);
                }

                return _brand;
            }
        }

        public ICartRepository Cart
        {
            get
            {
                if (_cart == null)
                {
                    _cart = new CartRepository(_repositoryContext);
                }

                return _cart;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_repositoryContext);
                }

                return _category;
            }
        }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_repositoryContext);
                }

                return _product;
            }
        }

        public ISubcategoryRepository Subcategory
        {
            get
            {
                if (_subcategory == null)
                {
                    _subcategory = new SubcategoryRepository(_repositoryContext);
                }

                return _subcategory;
            }
        }

        public IWishlistRepository Wishlist
        {
            get
            {
                if (_wishlist == null)
                {
                    _wishlist = new WishlistRepository(_repositoryContext);
                }

                return _wishlist;
            }
        }

        public RepositoryWrapper(TechStoreContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
