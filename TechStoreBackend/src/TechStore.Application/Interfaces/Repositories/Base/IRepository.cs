using TechStore.Domain.Entities.Base;


namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
