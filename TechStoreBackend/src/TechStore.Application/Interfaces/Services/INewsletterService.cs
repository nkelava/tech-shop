using TechStore.Application.Models.Newsletter;


namespace TechStore.Application.Interfaces.Services
{
    public interface INewsletterService
    {
        Task<NewsletterReadModel?> Subscribe(NewsletterCreateModel subscription);
        Task<NewsletterReadModel?> Unsubscribe(string email);
        
        Task<IEnumerable<NewsletterReadModel>> GetAllSubscribersAsync();
    }
}
