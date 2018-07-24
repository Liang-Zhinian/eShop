using System;
using SaaSEqt.IdentityAccess.Api.ViewModel;

namespace SaaSEqt.IdentityAccess.Api.Services
{
    public interface ITenantService
    {
        void ProvisionTenant(TenantViewModel tenant, StaffViewModel administrator);
        //void ModifyTenantAddress(TenantAddressViewModel addressViewModel);
        //void AddTenantAddress(TenantAddressViewModel addressViewModel);
    }
}
