using System;
using CqrsFramework.Events;
using SaaSEqt.eShop.Business.API.Application.Events;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Application.Commands;

using SaaSEqt.eShop.Business.API.ViewModel;

namespace SaaSEqt.eShop.Business.API.Infrastructure.Services
{
    public class TenantService: ITenantService
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IdentityApplicationService _identityApplicationService;

        public TenantService(IEventPublisher eventPublisher,
                             IdentityApplicationService identityApplicationService)
        {
            _eventPublisher = eventPublisher;
            _identityApplicationService = identityApplicationService;
        }

        public void ProvisionTenant(TenantViewModel tenant, StaffViewModel administrator)
        {
            ProvisionTenantCommand command = new ProvisionTenantCommand(
                        tenant.Name,
                        tenant.Description,
                        administrator.FirstName,
                        administrator.LastName,
                        administrator.EmailAddress,
                        administrator.PrimaryTelephone,
                        administrator.SecondaryTelephone,
                        administrator.AddressStreetAddress,
                        administrator.AddressCity,
                        administrator.AddressStateProvince,
                        administrator.AddressPostalCode,
                        administrator.AddressCountryCode
                    );

            var _tenant = _identityApplicationService.ProvisionTenant(command).Result;

            TenantCreatedEvent tenantCreatedEvent = new TenantCreatedEvent(
                _tenant.Id,
                _tenant.Name,
                _tenant.Description
            );

            _eventPublisher.Publish<TenantCreatedEvent>(tenantCreatedEvent);
        }

    }
}
