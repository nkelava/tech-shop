using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.Order;


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

        public OrderWithProductsSpecification(string email)
            : base(o => o.Email.Equals(email))
        {
            AddInclude(o => o.Products);
        }
    }
}
