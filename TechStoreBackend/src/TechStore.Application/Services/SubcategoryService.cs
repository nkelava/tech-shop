using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Subcategory;
using TechStore.Domain.Entities.SubcategoryAggregate;


namespace TechStore.Application.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public SubcategoryService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task CreateAsync(SubcategoryCreateModel createModel)
        {
            var subcategory = _mapper.Map<Subcategory>(createModel);

            _repository.Subcategory.Add(subcategory);
            await _repository.SaveAsync();
        }

        public async Task<SubcategoryReadModel?> UpdateAsync(int id, SubcategoryUpdateModel updateModel)
        {
            var existingSubcategory = await _repository.Subcategory.GetByIdAsync(id);

            if (existingSubcategory == null)
                return null;

            _mapper.Map(updateModel, existingSubcategory);
            _repository.Subcategory.Update(existingSubcategory);
            await _repository.SaveAsync();

            var subcategoryModel = _mapper.Map<SubcategoryReadModel>(existingSubcategory);
            return subcategoryModel;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var subcategory = await _repository.Subcategory.GetByIdAsync(id);

            if (subcategory == null)
                return null;

            _repository.Subcategory.Delete(subcategory);
            await _repository.SaveAsync();

            return subcategory.Id;
        }

        public async Task<SubcategoryReadModel?> GetByIdAsync(int id)
        {
            var subcategory = await _repository.Subcategory.GetByIdAsync(id);

            if (subcategory == null)
                return null;

            var subcategoryModel = _mapper.Map<SubcategoryReadModel>(subcategory);
            return subcategoryModel;
        } 
        
        public async Task<SubcategoryReadModel?> GetBySlugAsync(string slug)
        {
            var subcategory = await _repository.Subcategory.GetBySlugAsync(slug);

            if (subcategory == null)
                return null;

            var subcategoryModel = _mapper.Map<SubcategoryReadModel>(subcategory);
            return subcategoryModel;
        }

        public async Task<IEnumerable<SubcategoryReadModel>> GetAllAsync()
        {
            var subcategories = await _repository.Subcategory.GetAllAsync();
            var subcategoriesModel = _mapper.Map<IList<SubcategoryReadModel>>(subcategories);

            return subcategoriesModel;
        }
    }
}
