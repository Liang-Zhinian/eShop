﻿using System;
using CqrsFramework.Events;
using SaaSEqt.eShop.Services.Business.API.Application.Events;
using SaaSEqt.IdentityAccess.Application;
using SaaSEqt.IdentityAccess.Application.Commands;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;

using SaaSEqt.eShop.Services.Business.API.ViewModel;

namespace SaaSEqt.eShop.Services.Business.API.Infrastructure.Services
{
    public class TenantService: ITenantService
    {
        //private readonly IEventPublisher _eventPublisher;
        private readonly IdentityApplicationService _identityApplicationService;

        public TenantService(//IEventPublisher eventPublisher,
                             IdentityApplicationService identityApplicationService)
        {
            //_eventPublisher = eventPublisher;
            _identityApplicationService = identityApplicationService;
        }

        public Tenant ProvisionTenant(TenantViewModel tenant, StaffViewModel administrator)
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
            return _tenant;
        }

    }
}