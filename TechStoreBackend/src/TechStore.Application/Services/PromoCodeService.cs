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


        public async Task CreateAsync(PromoCodeCreateModel createModel)
        {
            var promoCode = _mapper.Map<PromoCode>(createModel);

            _repository.PromoCode.Add(promoCode);
            await _repository.SaveAsync();
        }

        public async Task<PromoCodeReadModel?> UpdateAsync(int id, PromoCodeUpdateModel updateModel)
        {
            var existingPromoCode = await _repository.PromoCode.GetByIdAsync(id);

            if (existingPromoCode == null)
                return null;

            _mapper.Map(updateModel, existingPromoCode);
            _repository.PromoCode.Update(existingPromoCode);
            await _repository.SaveAsync();

            var promoCodeModel = _mapper.Map<PromoCodeReadModel>(existingPromoCode);
            return promoCodeModel;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var promoCode = await _repository.PromoCode.GetByIdAsync(id);

            if (promoCode == null)
                return null;

            _repository.PromoCode.Delete(promoCode);
            await _repository.SaveAsync();

            return promoCode.Id;
        }

        public async Task<PromoCodeReadModel?> GetByIdAsync(int id)
        {
            var promoCode = await _repository.PromoCode.GetByIdAsync(id);

            if (promoCode == null)
                return null;

            var promoCodeModel = _mapper.Map<PromoCodeReadModel>(promoCode);
            return promoCodeModel;
        }

        public async Task<PromoCodeReadModel?> GetByCodeAsync(string code)
        {
            var promoCode = await _repository.PromoCode.GetByCodeAsync(code);

            if (promoCode == null)
                return null;

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
