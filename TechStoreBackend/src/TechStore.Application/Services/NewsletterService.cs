using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.Newsletter;
using TechStore.Domain.Entities;


namespace TechStore.Application.Services
{
    public class NewsletterService : INewsletterService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public NewsletterService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<NewsletterReadModel?> Subscribe(NewsletterCreateModel subscriptionModel)
        {
            var existingSubscription = _repository.Newsletter.FindByCondition(s => s.Email == subscriptionModel.Email);

            if (existingSubscription != null)
                return null;

            var subsription = _mapper.Map<Newsletter>(subscriptionModel);

            _repository.Newsletter.Add(subsription);
            await _repository.SaveAsync();

            var newsletterReadModel = _mapper.Map<NewsletterReadModel>(subsription);
            return newsletterReadModel;
        }

        public async Task<NewsletterReadModel?> Unsubscribe(string email)
        {
            var subscription = _repository.Newsletter.FindByCondition(s => s.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();

            if (subscription == null)
                return null;

            _repository.Newsletter.Delete(subscription);
            await _repository.SaveAsync();

            var subscriptionModel = _mapper.Map<NewsletterReadModel>(subscription);
            return subscriptionModel;
        }

        public async Task<IEnumerable<NewsletterReadModel>> GetAllSubscribersAsync()
        {
            var subscibers = await _repository.Newsletter.GetAllSubscribersAsync();
            var subscribersModel = _mapper.Map<IList<NewsletterReadModel>>(subscibers);

            return subscribersModel;
        }
    }
}
