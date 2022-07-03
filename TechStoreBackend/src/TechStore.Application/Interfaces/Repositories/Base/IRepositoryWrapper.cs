

namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryWrapper
    {
        IBrandRepository Brand { get; }
        ISubcategoryRepository Subcategory { get; }
        ICategoryRepository Category { get; }

        Task SaveAsync();
    }
}
