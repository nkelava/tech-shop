using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Product;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Services
{
    public class ProductService : IProductService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductCreateModel productModel)
        {
            var productMapped = _mapper.Map<Product>(productModel);
            
            _repository.Product.Add(productMapped);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _repository.Product.GetProductByIdAsync(productId);

            _repository.Product.Delete(product);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(ProductUpdateModel product)
        {
            var movieMapped = _mapper.Map<Product>(product);

            _repository.Product.Update(movieMapped);
            await _repository.SaveAsync();
        }

        public async Task<ProductReadModel> GetProductByIdAsync(int productId)
        {
            var product = await _repository.Product.GetProductByIdAsync(productId);
            var productMapped = _mapper.Map<ProductReadModel>(product);

            return productMapped;
        }

        public async Task<ProductReadModel> GetProductBySlugAsync(string slug)
        {
            var product = await _repository.Product.GetProductBySlugAsync(slug);
            var productMapped = _mapper.Map<ProductReadModel>(product);

            return productMapped;
        }

        public async Task<IList<ProductReadModel>> GetAllProductsAsync()
        {
            var products = await _repository.Product.GetAllProductsAsync();
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsOnSaleAsync()
        {
            var products = await _repository.Product.GetProductsOnSaleAsync();
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsByBrandIdAsync(int brandId)
        {
            var products = await _repository.Product.GetProductsByBrandIdAsync(brandId);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsByBrandNameAsync(string brandName)
        {
            var products = await _repository.Product.GetProductsByBrandNameAsync(brandName);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            var products = await _repository.Product.GetProductsBySubcategoryIdAsync(subcategoryId);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsBySubcategoryNameAsync(string subcategoryName)
        {
            var products = await _repository.Product.GetProductsBySubcategoryNameAsync(subcategoryName);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsByNameAsync(string productsName)
        {
            var products = await _repository.Product.GetProductsByNameAsync(productsName);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo)
        {
            var products = await _repository.Product.GetProductsByPriceAsync(priceFrom, priceTo);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IList<ProductReadModel>> GetProductsByRatingAsync(decimal rating)
        {
            var products = await _repository.Product.GetProductsByRatingAsync(rating);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }
    }
}
