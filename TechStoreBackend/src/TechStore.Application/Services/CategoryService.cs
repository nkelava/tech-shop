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


        public async Task<CategoryReadModel?> CreateAsync(CategoryCreateModel createModel)
        {
            var existingCategory = await _repository.Category.GetBySlugAsync(createModel.Slug);

            if (existingCategory != null)
                return null;

            var category = _mapper.Map<Category>(createModel);

            _repository.Category.Add(category);
            await _repository.SaveAsync();

            var categoryModel = _mapper.Map<CategoryReadModel>(category);
            return categoryModel;
        }

        public async Task<CategoryReadModel?> UpdateAsync(int id, CategoryUpdateModel updateModel)
        {
            var existingCategory = await _repository.Category.GetByIdAsync(id);

            if (existingCategory == null)
                return null;

            _mapper.Map(updateModel, existingCategory);
            _repository.Category.Update(existingCategory);
            await _repository.SaveAsync();

            var categoryModel = _mapper.Map<CategoryReadModel>(existingCategory);
            return categoryModel;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var category = await _repository.Category.GetByIdAsync(id);

            if (category == null)
                return null;

            _repository.Category.Delete(category);
            await _repository.SaveAsync();

            return category.Id;
        }

        public async Task<CategoryReadModel?> GetByIdAsync(int id)
        {
            var category = await _repository.Category.GetByIdAsync(id);

            if (category == null)
                return null;
            
            var categoryModel = _mapper.Map<CategoryReadModel>(category);
            return categoryModel;
        }

        public async Task<CategoryReadModel?> GetBySlugAsync(string slug)
        {
            var category = await _repository.Category.GetBySlugAsync(slug);

            if (category == null)
                return null;

            var categoryModel = _mapper.Map<CategoryReadModel>(category);
            return categoryModel;
        }

        public async Task<CategoryWithSubcategoriesModel?> GetWithSubcategoriesAsync(string slug)
        {
            var category = await _repository.Category.GetBySlugAsync(slug);

            if (category == null)
                return null;

            var categoryWithSubcategories = await _repository.Category.GetWithSubcategoriesAsync(category.Id);
            var categoryModel = _mapper.Map<CategoryWithSubcategoriesModel>(categoryWithSubcategories);

            return categoryModel;
        }

        public async Task<IEnumerable<CategoryReadModel>> GetAllAsync()
        {
            var categories = await _repository.Category.GetAllAsync();
            var categoriesModel = _mapper.Map<IList<CategoryReadModel>>(categories);

            return categoriesModel;
        }
    }
}
