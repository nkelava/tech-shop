

namespace TechStore.Application.Interfaces.Repositories.Base
{
    public interface IRepositoryWrapper
    {
        IBrandRepository Brand { get; }

        Task SaveAsync();
    }
}
