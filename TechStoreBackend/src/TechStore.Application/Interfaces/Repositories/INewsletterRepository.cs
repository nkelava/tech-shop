using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
        IList<Newsletter> GetAllNewsletterSubscribers();
    }
}
