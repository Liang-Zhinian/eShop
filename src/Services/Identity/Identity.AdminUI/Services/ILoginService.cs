﻿using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.IdentityManagement.API.Services
{
    public interface ILoginService<T>
    {
        Task<bool> ValidateCredentials(T user, string password);
        Task<T> FindByUsername(string user);
        Task SignIn(T user);
    }
}
