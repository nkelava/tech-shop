﻿using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand> GetBrandByIdAsync(int brandId);

        Task<IEnumerable<Brand>> GetAllBrandsAsync();
    }
}
