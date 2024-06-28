using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Product;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Domain.Entities.SubcategoryAggregate;


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


        public async Task CreateAsync(ProductCreateModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);

            _repository.Product.Add(product);
            await _repository.SaveAsync();
        }

        public async Task<ProductReadModel?> AddSpecificationAsync(int id, List<ProductAttributeSetModel> productAttributes)
        {
            var product = await _repository.Product.GetByIdAsync(id);

            if (product == null)
                return null;

            foreach (var attributeSet in productAttributes)
            {
                var newProductSet = new ProductAttributeSet
                {
                    ProductId = product.Id,
                    AttributeId = attributeSet.AttributeId,
                    AttributeValueId = attributeSet.AttributeValueId,
                };

                _repository.ProductAttributeSet.Add(newProductSet);
            }

            await _repository.SaveAsync();

            var productModel = _mapper.Map<ProductReadModel>(product);
            return productModel;
        }

        public async Task<ProductReadModel?> UpdateAsync(int id, ProductUpdateModel product)
        {
            var existingProduct = await _repository.Product.GetByIdAsync(id);

            if (existingProduct == null)
                return null;

            _mapper.Map(product, existingProduct);

            _repository.Product.Update(existingProduct);
            await _repository.SaveAsync();

            var productModel = _mapper.Map<ProductReadModel>(existingProduct);
            return productModel;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var product = await _repository.Product.GetByIdAsync(id);

            if (product == null)
                return null;

            _repository.Product.Delete(product);
            await _repository.SaveAsync();
            
            return product.Id;
        }

        public async Task<ProductReadModel?> GetByIdAsync(int id)
        {
            var product = await _repository.Product.GetByIdAsync(id);

            if (product == null)
                return null;

            var productMapped = _mapper.Map<ProductReadModel>(product);
            return productMapped;
        }

        public async Task<ProductReadModel?> GetBySlugAsync(string slug)
        {
            var product = await _repository.Product.GetBySlugAsync(slug);

            if (product == null)
                return null;

            var productMapped = _mapper.Map<ProductReadModel>(product);
            return productMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> GetAllProductsAsync()
        {
            var products = await _repository.Product.GetAllProductsAsync();
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> GetHotOffersAsync()
        {
            var products = await _repository.Product.GetHotOffersAsync();
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> GetNewProductsAsync()
        {
            var products = await _repository.Product.GetNewProductsAsync();
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> GetTopSellingProductsAsync()
        {
            var products = await _repository.Product.GetTopSellingProductsAsync();
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> GetTopRatedProductsAsync()
        {
            var products = await _repository.Product.GetTopRatedProductsAsync();
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>?> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            var subcategory = await _repository.Subcategory.GetByIdAsync(subcategoryId);

            if (subcategory == null)
                return null;

            var products = await _repository.Product.GetProductsBySubcategoryIdAsync(subcategoryId);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>?> GetProductsBySubcategorySlugAsync(string subcategorySlug)
        {
            var subcategory = await _repository.Subcategory.GetBySlugAsync(subcategorySlug);

            if (subcategory == null)
                return null;

            var products = await _repository.Product.GetProductsBySubcategorySlugAsync(subcategorySlug);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> SearchProductsAsync(string search)
        {
            var products = await _repository.Product.SearchProductsAsync(search);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> GetProductsByPriceAsync(decimal priceFrom, decimal priceTo)
        {
            var products = await _repository.Product.GetProductsByPriceAsync(priceFrom, priceTo);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }

        public async Task<IEnumerable<ProductReadModel>> GetProductsByRatingAsync(decimal rating)
        {
            var products = await _repository.Product.GetProductsByRatingAsync(rating);
            var productsMapped = _mapper.Map<IList<ProductReadModel>>(products);

            return productsMapped;
        }
    }
}
