using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class NewsletterRepository : Repository<Newsletter>, INewsletterRepository
    {
        public NewsletterRepository(TechStoreContext techStoreContext)
            : base(techStoreContext) { }

        public IList<Newsletter> GetAllNewsletterSubscribers()
        {
            return FindAll().ToList();
        }

    }
}
