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
            var product = await FindByCondition(p => p.Slug.ToLower().Equals(slug.ToLower()))
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.Attribute)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.AttributeValue)
                .FirstOrDefaultAsync();
            
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var spec = new ProductsWithSubcategorySpecification();
            return await Find(spec).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsOnSaleAsync()
        {
            return await FindByCondition(p => p.OnSale.Equals(true)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetNewProductsAsync()
        {
            var spec = new ProductsWithSubcategorySpecification();
            return await Find(spec).OrderByDescending(p => p.CreatedAt).Take(12).ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> GetTopSellingProductsAsync()
        {
            var spec = new ProductsWithSubcategorySpecification();
            return await Find(spec).OrderByDescending(p => p.UnitsSold).Take(12).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetTopRatedProductsAsync()
        {
            var spec = new ProductsWithSubcategorySpecification();
            return await Find(spec).OrderByDescending(p => p.Rating).Take(12).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            var spec = new ProductsWithSubcategorySpecification(subcategoryId);
            var products = await Find(spec)
              .Include(p => p.ProductAttributes)
                  .ThenInclude(pas => pas.Attribute)
              .Include(p => p.ProductAttributes)
                  .ThenInclude(pas => pas.AttributeValue)
              .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsBySubcategorySlugAsync(string subcategorySlug)
        {
            var spec = new ProductsWithSubcategorySpecification(subcategorySlug);
            //var products = await Find(spec).ToListAsync();
            var products = await Find(spec)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.Attribute)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.AttributeValue)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
        {
            return await FindByCondition(p => p.Name.ToLower().Contains(productName.ToLower())).ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo)
        {   
            return await FindByCondition(p => p.Price >= priceFrom && p.Price <= priceTo).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByRatingAsync(decimal rating)
        {
            return await FindByCondition(p => p.Rating.Equals(rating)).ToListAsync();
        }
    }
}
