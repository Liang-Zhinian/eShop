using System;
using SaaSEqt.eShop.Business.API.ViewModel;

namespace SaaSEqt.eShop.Business.API.Infrastructure.Services
{
    public interface ITenantService
    {
        void ProvisionTenant(TenantViewModel tenant, StaffViewModel administrator);
        //void ModifyTenantAddress(TenantAddressViewModel addressViewModel);
        //void AddTenantAddress(TenantAddressViewModel addressViewModel);
    }
}
