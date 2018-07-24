using System;
using CqrsFramework.Events;
using SaaSEqt.IdentityAccess.API.Events;
using SaaSEqt.IdentityAccess.API.ViewModel;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Application.Commands;

namespace SaaSEqt.IdentityAccess.API.Services
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
