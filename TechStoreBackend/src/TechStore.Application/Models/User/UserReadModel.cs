using TechStore.Application.Models.Base;
using TechStore.Domain.Entities.User;


namespace TechStore.Application.Models.User
{
    public class UserReadModel
    {
        public ApplicationUser Info { get; set; }
        public bool IsAdmin { get; set; }
    }
}
