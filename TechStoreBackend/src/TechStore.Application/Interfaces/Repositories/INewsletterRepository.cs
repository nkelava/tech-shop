using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
        Task<IEnumerable<Newsletter>> GetAllSubscribersAsync();
    }
}
