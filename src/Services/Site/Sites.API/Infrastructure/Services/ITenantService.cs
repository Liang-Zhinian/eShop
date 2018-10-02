using System;
using SaaSEqt.eShop.Services.Sites.API.ViewModel;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.eShop.Services.Sites.API.Infrastructure.Services
{
    public interface ITenantService
    {
        Tenant ProvisionTenant(TenantViewModel tenant, StaffViewModel administrator);
        //void ModifyTenantAddress(TenantAddressViewModel addressViewModel);
        //void AddTenantAddress(TenantAddressViewModel addressViewModel);
    }
}
