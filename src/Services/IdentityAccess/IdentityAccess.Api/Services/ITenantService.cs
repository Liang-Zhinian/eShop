using System;
using SaaSEqt.IdentityAccess.API.ViewModel;

namespace SaaSEqt.IdentityAccess.API.Services
{
    public interface ITenantService
    {
        void ProvisionTenant(TenantViewModel tenant, StaffViewModel administrator);
        //void ModifyTenantAddress(TenantAddressViewModel addressViewModel);
        //void AddTenantAddress(TenantAddressViewModel addressViewModel);
    }
}
