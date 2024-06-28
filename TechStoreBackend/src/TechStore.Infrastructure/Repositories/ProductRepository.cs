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


        public async Task<Product?> GetByIdAsync(int id) {
            return await FindByCondition(p => p.Id.Equals(id))
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync();
        }

        public async Task<Product?> GetByIdWithoutSubcategoryAsync(int  id)
        {
            return await FindByCondition(p => p.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Product?> GetBySlugAsync(string slug)
        {
            return await FindByCondition(p => p.Slug.ToLower().Equals(slug.ToLower()))
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.Attribute)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.AttributeValue)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var spec = new ProductsWithSubcategorySpecification();
            return await Find(spec).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetHotOffersAsync()
        {
            var spec = new ProductsWithSubcategorySpecification();
            var products = await Find(spec)
                .ToListAsync();

            var sortedProducts = products.OrderByDescending(p => CalculateScore(p)).Take(12);
            return sortedProducts;
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
                .Include(p => p.Subcategory)
                    .ThenInclude(s => s.Category)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.Attribute)
                .Include(p => p.ProductAttributes)
                    .ThenInclude(pas => pas.AttributeValue)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string search)
        {
            return await FindByCondition(p => p.Summary.ToLower().Contains(search.ToLower()))
                .Include(p => p.Subcategory)
                    .ThenInclude(s => s.Category)
                .ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo)
        {   
            return await FindByCondition(p => p.Price >= priceFrom && p.Price <= priceTo).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByRatingAsync(decimal rating)
        {
            return await FindByCondition(p => p.Rating.Equals(rating)).ToListAsync();
        }

        private static decimal CalculateScore(Product product)
        {
            decimal score = 0;

            score += product.UnitsSold * 0.5m;
            score += product.Discount * 0.3m;
            score += product.Rating * 0.2m;

            return score;
        }
    }
}
