using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.OrderAggregate;


namespace TechStore.Application.Specifications.OrderSpecification
{
    public class OrderWithProductsSpecification : BaseSpecification<Order>
    {
        public OrderWithProductsSpecification()
        {
        }

        public OrderWithProductsSpecification(int id)
            : base(o => o.Id.Equals(id))
        {
            AddInclude(o => o.Products);
        }

        public OrderWithProductsSpecification(string userId)
            : base(o => o.ApplicationUserId != null && o.ApplicationUserId.Equals(userId))
        {
            AddInclude(o => o.Products);
            AddInclude(o => o.DeliveryAddress);
        }
    }
}
