using System;
using SaaSEqt.eShop.Services.IdentityAccess.API.ViewModel;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

namespace SaaSEqt.eShop.Services.IdentityAccess.API.Infrastructure.Services
{
    public interface ITenantService
    {
        Tenant ProvisionTenant(TenantViewModel tenant, StaffViewModel administrator);
        RegistrationInvitation OfferRegistrationInvitation(Guid tenantId, string description);
        //void ModifyTenantAddress(TenantAddressViewModel addressViewModel);
        //void AddTenantAddress(TenantAddressViewModel addressViewModel);
    }
}
