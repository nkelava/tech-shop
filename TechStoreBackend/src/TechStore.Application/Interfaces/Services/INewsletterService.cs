using TechStore.Application.Models.Newsletter;


namespace TechStore.Application.Interfaces.Services
{
    public interface INewsletterService
    {
        Task Subscribe(string email);
        Task Unsubscribe(string email);

        IList<NewsletterReadModel> GetAllNewsletterSubsribers();
    }
}
