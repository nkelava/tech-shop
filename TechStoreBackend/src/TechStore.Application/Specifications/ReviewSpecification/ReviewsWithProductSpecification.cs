using TechStore.Application.Specifications.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Application.Specifications.ReviewSpecification
{
    public class ReviewsWithProductSpecification : BaseSpecification<Review>
    {
        public ReviewsWithProductSpecification()
            : base()
        {
            AddInclude(r => r.Product);
        }

        public ReviewsWithProductSpecification(int productId)
           : base(r => r.ProductId.Equals(productId))
        {
            AddInclude(r => r.Product);
        }

        public ReviewsWithProductSpecification(string email)
            : base(r => r.Email.ToLower().Equals(email.ToLower()))
        {
            AddInclude(r => r.Product);
        }
    }
}
