﻿using TechStore.Application.Interfaces.Repositories.Base;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Application.Interfaces.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task<Wishlist> GetWishlistByUsernameAsync(string username);
    }
}
