﻿using AutoMapper;
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

        public async Task AddAsync(SubcategoryCreateModel subcategoryModel)
        {
            var subcategory = _mapper.Map<Subcategory>(subcategoryModel);

            _repository.Subcategory.Add(subcategory);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(SubcategoryUpdateModel subcategoryModel)
        {
            var subcategory = _mapper.Map<Subcategory>(subcategoryModel);
            
            _repository.Subcategory.Update(subcategory);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int subcategoryId)
        {
            var subcategory= await _repository.Subcategory.GetSubcategoryByIdAsync(subcategoryId);

            _repository.Subcategory.Delete(subcategory);
            await _repository.SaveAsync();
        }

        public async Task<SubcategoryReadModel> GetSubcategoryByIdAsync(int subcategoryId)
        {
            var subcategory = await _repository.Subcategory.GetSubcategoryByIdAsync(subcategoryId);
            var subcategoryReadModel = _mapper.Map<SubcategoryReadModel>(subcategory);

            return subcategoryReadModel;
        }

        public async Task<IList<SubcategoryReadModel>> GetAllSubcategoriesAsync()
        {
            var subcategories = await _repository.Subcategory.GetAllSubcategoriesAsync();
            var subcategoriesReadModel = _mapper.Map<IList<SubcategoryReadModel>>(subcategories);

            return subcategoriesReadModel;
        }
    }
}
