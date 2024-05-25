using TechStore.Application.Interfaces.Repositories;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Repositories.Base;


namespace TechStore.Infrastructure.Repositories
{
    public class ProductAttributeSetRepository : Repository<ProductAttributeSet>, IProductAttributeSetRepository
    {
        public ProductAttributeSetRepository(TechStoreContext techStoreContext)
                : base(techStoreContext) { }
    }
}
