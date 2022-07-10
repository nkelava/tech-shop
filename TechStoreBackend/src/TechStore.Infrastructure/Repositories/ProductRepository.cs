using Microsoft.EntityFrameworkCore;
using TechStore.Application.Interfaces.Repositories;
using TechStore.Application.Specifications.ProductSpecification;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(TechStoreContext techStoreContext)
            : base(techStoreContext) { }

        public async Task<Product> GetProductByIdAsync(int productId) {
            var product = await FindByCondition(p => p.Id.Equals(productId)).FirstOrDefaultAsync();

            return product;
        }
        public async Task<Product> GetProductBySlugAsync(string slug)
        {
            var product = await FindByCondition(p => p.Slug.ToLower().Equals(slug.ToLower())).FirstOrDefaultAsync();
            
            return product;
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            var products = await FindAll().ToListAsync();

            return products;
        }

        public async Task<IList<Product>> GetProductsOnSaleAsync()
        {
            var products = await FindByCondition(p => p.OnSale.Equals(true)).ToListAsync();

            return products;
        }

        public async Task<IList<Product>> GetProductsByBrandIdAsync(int brandId)
        {
            var spec = new ProductsWithBrandSpecification(brandId);
            var products = await Find(spec).ToListAsync();

            return products;
        }

        public async Task<IList<Product>> GetProductsByBrandNameAsync(string brandName)
        {
            var spec = new ProductsWithBrandSpecification(brandName);
            var products = await Find(spec).ToListAsync();

            return products;
        }

        public async Task<IList<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            var spec = new ProductsWithSubcategorySpecification(subcategoryId);
            var products = await Find(spec).ToListAsync();

            return products;
        }

        public async Task<IList<Product>> GetProductsBySubcategoryNameAsync(string subcategoryName)
        {
            var spec = new ProductsWithSubcategorySpecification(subcategoryName);
            var products = await Find(spec).ToListAsync();

            return products;
        }

        //public async Task<IEnumerable<Product>> GetProductsByPropertyIdAsync(int propertyId)
        //{
        //    var spec = new ProductsWithPropertySpecification(propertyId);
        //    var products = await Find(spec).ToListAsync();

        //    return products;
        //}

        //public async Task<IEnumerable<Product>> GetProductsByPropertyNameAsync(string propertyName)
        //{
        //    var spec = new ProductsWithPropertySpecification(propertyName);
        //    var products = await Find(spec).ToListAsync();

        //    return products;
        //}

        public async Task<IList<Product>> GetProductsByNameAsync(string productName)
        {
            var products = await FindByCondition(p => p.Name.ToLower().Contains(productName.ToLower())).ToListAsync();

            return products;
        }


        public async Task<IList<Product>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo)
        {   
            var products = await FindByCondition(p => p.Price >= priceFrom && p.Price <= priceTo).ToListAsync();

            return products;
        }

        public async Task<IList<Product>> GetProductsByRatingAsync(decimal rating)
        {
            var products = await FindByCondition(p => p.Rating.Equals(rating)).ToListAsync();

            return products;
        }
    }
}
