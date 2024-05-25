using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Infrastructure.Data;


namespace TechStore.Infrastructure.Repositories.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TechStoreContext _repositoryContext;
        private IAttributeRepository _attribute;
        private IAttributeValueRepository _attributeValue;
        private ICartRepository _cart;
        private ICategoryRepository _category;
        private INewsletterRepository _newsletter;
        private IOrderRepository _order;
        private IProductRepository _product;
        private IProductAttributeSetRepository _productAttributeSet;
        private IPromoCodeRepository _promoCode;
        private IReviewRepository _review;
        private ISubcategoryRepository _subcategory;
        private IWishlistRepository _wishlist;

        public IAttributeRepository Attribute
        {
            get
            {
                if (_attribute == null)
                {
                    _attribute = new AttributeRepository(_repositoryContext);
                }

                return _attribute;
            }
        }

        public IAttributeValueRepository AttributeValue
        {
            get
            {
                if (_attributeValue == null)
                {
                    _attributeValue = new AttributeValueRepository(_repositoryContext);
                }

                return _attributeValue;
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

        public INewsletterRepository Newsletter
        {
            get
            {
                if (_newsletter == null)
                {
                    _newsletter = new NewsletterRepository(_repositoryContext);
                }

                return _newsletter;
            }
        }

        public IOrderRepository Order
        {
            get
            {
                if(_order == null)
                {
                    _order = new OrderRepository(_repositoryContext);
                }

                return _order;
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
        
        public IProductAttributeSetRepository ProductAttributeSet
        {
            get
            {
                if (_productAttributeSet == null)
                {
                    _productAttributeSet = new ProductAttributeSetRepository(_repositoryContext);
                }

                return _productAttributeSet;
            }
        }

        public IPromoCodeRepository PromoCode
        {
            get
            {
                if (_promoCode == null)
                {
                    _promoCode = new PromoCodeRepository(_repositoryContext);
                }

                return _promoCode;
            }
        }

        public IReviewRepository Review
        {
            get
            {
                if (_review == null)
                {
                    _review = new ReviewRepository(_repositoryContext);
                }

                return _review;
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
