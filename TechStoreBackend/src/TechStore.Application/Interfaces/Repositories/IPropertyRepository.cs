using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<Property> GetPropertyByIdAsync(int id);
        Task<Property> GetPropertyByNameAsync(string name);
    }
}
