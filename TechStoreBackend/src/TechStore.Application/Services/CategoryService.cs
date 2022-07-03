using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Category;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateCategory(CategoryCreateModel categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);

            _repository.Category.Create(category);
            await _repository.SaveAsync();
        }

        public async Task UpdateCategory(CategoryUpdateModel categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);
            _repository.Category.Update(category);

            await _repository.SaveAsync();
        }

        public async Task DeleteCategory(int brandId)
        {
            var category = await _repository.Category.GetCategoryByIdAsync(brandId);

            _repository.Category.Delete(category);
            await _repository.SaveAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int brandId)
        {
            var category = await _repository.Category.GetCategoryByIdAsync(brandId);

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _repository.Category.GetAllCategoriesAsync();

            return categories;
        }
    }
}
