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

        public async Task AddAsync(CategoryCreateModel categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);

            _repository.Category.Add(category);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(CategoryUpdateModel categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);
            _repository.Category.Update(category);

            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _repository.Category.GetCategoryByIdAsync(categoryId);

            _repository.Category.Delete(category);
            await _repository.SaveAsync();
        }

        public async Task<CategoryReadModel> GetCategoryBySlugAsync(string categorySlug)
        {
            var category = await _repository.Category.GetCategoryBySlugAsync(categorySlug);
            var categoryModel = _mapper.Map<CategoryReadModel>(category);

            return categoryModel;
        }

        public async Task<CategoryWithSubcategoriesModel> GetCategoryWithSubcategoriesAsync(string categorySlug)
        {
            var category = await _repository.Category.GetCategoryBySlugAsync(categorySlug);
            var categoryWithSubcategories = await _repository.Category.GetCategoryWithSubcategoriesAsync(category.Id);
            var categoryModel = _mapper.Map<CategoryWithSubcategoriesModel>(categoryWithSubcategories);

            return categoryModel;
        }

        public async Task<IList<CategoryReadModel>> GetAllCategoriesAsync()
        {
            var categories = await _repository.Category.GetAllCategoriesAsync();
            var categoriesModel = _mapper.Map<IList<CategoryReadModel>>(categories);

            return categoriesModel;
        }
    }
}
