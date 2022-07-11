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

        public async Task Subscribe(string email)
        {
            _repository.Newsletter.Add(new Newsletter
            {
                Email = email
            });

            await _repository.SaveAsync();
        }

        public async Task Unsubscribe(string email)
        {
            var subscription = _repository.Newsletter.FindByCondition(n => n.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();

            if (subscription == null)
                return;

            _repository.Newsletter.Delete(subscription);

            await _repository.SaveAsync();
        }

        public IList<NewsletterReadModel> GetAllNewsletterSubsribers()
        {
            var subscibers = _repository.Newsletter.GetAllNewsletterSubscribers();
            var subscribersModel = _mapper.Map<IList<NewsletterReadModel>>(subscibers);

            return subscribersModel;
        }
    }
}
