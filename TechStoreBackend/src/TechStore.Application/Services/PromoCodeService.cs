using AutoMapper;
using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Application.Interfaces.Services;
using TechStore.Application.Models.PromoCode;
using TechStore.Domain.Entities;


namespace TechStore.Application.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        public readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public PromoCodeService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PromoCodeCreateModel promoCodeModel)
        {
            var promoCode = _mapper.Map<PromoCode>(promoCodeModel);
            _repository.PromoCode.Add(promoCode);

            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(PromoCodeUpdateModel promoCodeUpdateModel)
        {
            var promoCode = _mapper.Map<PromoCode>(promoCodeUpdateModel);

            _repository.PromoCode.Update(promoCode);
            await _repository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int promoCodeId)
        {
            var promoCode = await _repository.PromoCode.GetByIdAsync(promoCodeId);

            if (promoCode == null)
                return 0;

            _repository.PromoCode.Delete(promoCode);
            await _repository.SaveAsync();

            return promoCode.Id;
        }

        public async Task<PromoCodeReadModel> GetByIdAsync(int id)
        {
            var promoCode = await _repository.PromoCode.GetByIdAsync(id);
            var promoCodeModel = _mapper.Map<PromoCodeReadModel>(promoCode);

            return promoCodeModel;
        }

        public async Task<PromoCodeReadModel> GetByCodeAsync(string code)
        {
            var promoCode = await _repository.PromoCode.GetByCodeAsync(code);
            var promoCodeModel = _mapper.Map<PromoCodeReadModel>(promoCode);

            return promoCodeModel;
        }

        public async Task<IEnumerable<PromoCodeReadModel>> GetAllAsync()
        {
            var promoCodes= await _repository.PromoCode.GetAllAsync();
            var promoCodesModel = _mapper.Map<IList<PromoCodeReadModel>>(promoCodes);

            return promoCodesModel;
        }
    }
}
