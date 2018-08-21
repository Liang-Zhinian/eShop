using System;
using System.Collections.Generic;
//using CqrsFramework.Events;
using SaaSEqt.Common.Domain.Model;
using SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.Tenant;
using SaaSEqt.IdentityAccess.Contracts.IntegrationEvents.User;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
using SaaSEqt.IdentityAccess.Domain.Identity.Events.Tenant;
using SaaSEqt.IdentityAccess.Domain.Identity.Events.User;
using SaaSEqt.IdentityAccess.Domain.Identity.Repositories;

namespace SaaSEqt.IdentityAccess.Application
{
    public class IdentityAccessEventProcessor
    {
        //private readonly ILifetimeScope _autofac;
        readonly IIdentityAccessIntegrationEventService _identityAccessIntegrationEventService;
        //readonly IEventPublisher _eventPublisher;
        //readonly IGroupRepository groupRepository;
        //readonly ITenantRepository tenantRepository;
        //readonly IUserRepository userRepository;
        //readonly Common.Events.IEventStore eventStore;

        public IdentityAccessEventProcessor(
                                            //Common.Events.IEventStore eventStore,
                                            //IEventPublisher eventPublisher,
                                            IIdentityAccessIntegrationEventService identityAccessIntegrationEventService
                                            //IGroupRepository groupRepository,
                                            //ITenantRepository tenantRepository,
                                            //IUserRepository userRepository
                                            /*, ILifetimeScope autofac*/)
        {
            //this.eventStore = eventStore;
            //_autofac = autofac;
            //_eventPublisher = eventPublisher;
            _identityAccessIntegrationEventService = identityAccessIntegrationEventService;
            //this.groupRepository = groupRepository;
            //this.tenantRepository = tenantRepository;
            //this.userRepository = userRepository;
        }

        public void Listen()
        {
            DomainEventPublisher.Instance.Subscribe(domainEvent =>
                {
                    // to do: publish integration events

                    if (domainEvent is TenantProvisioned)
                    {
                        Console.WriteLine("To Do: TenantProvisionedEvent.");
                        TenantProvisioned evt = domainEvent as TenantProvisioned;
                        //var tenant = tenantRepository.Get(new TenantId(evt.TenantId));

                        TenantProvisionedIntegrationEvent tenantProvisionedEvent = new TenantProvisionedIntegrationEvent(
                            evt.TenantId,
                            evt.Name,
                            evt.Description,
                            evt.Active
                    );
                        tenantProvisionedEvent.TimeStamp = evt.TimeStamp;
                        tenantProvisionedEvent.Version = evt.Version;

                        //eventStore.Save(new List<IEvent> { tenantProvisionedEvent });
                        _identityAccessIntegrationEventService.PublishThroughEventBusAsync(tenantProvisionedEvent);
                        //return;
                    }

                    else if (domainEvent is TenantAdministratorRegistered)
                    {
                        Console.WriteLine("To Do: TenantAdministratorRegistered.");

                        TenantAdministratorRegistered evt = domainEvent as TenantAdministratorRegistered;
                        var tenantId = evt.TenantId;
                        //var user = userRepository.UserWithUsername(tenantId, evt.Name);

                        UserRegisteredIntegrationEvent userRegisteredIntegrationEvent = new UserRegisteredIntegrationEvent(
                                evt.TenantId,
                                evt.UserId,
                                evt.Name,
                                evt.AdministorName,
                                evt.EmailAddress
                            );
                        userRegisteredIntegrationEvent.TimeStamp = evt.TimeStamp;
                        userRegisteredIntegrationEvent.Version = evt.Version;

                        //eventStore.Save(new List<IEvent> { userRegisteredIntegrationEvent });
                        _identityAccessIntegrationEventService.PublishThroughEventBusAsync(userRegisteredIntegrationEvent);
                        //return;
                    }

                    else if (domainEvent is TenantActivated)
                    {
                        Console.WriteLine("To Do: TenantActivated.");
                        //return;
                    }

                    else if (domainEvent is TenantDeactivated)
                    {
                        Console.WriteLine("To Do: TenantDeactivated.");
                        //return;
                    }


                    else if (domainEvent is UserRegistered)
                    {
                        Console.WriteLine("To Do: UserRegistered.");

                        UserRegistered evt = domainEvent as UserRegistered;
                        var tenantId = evt.TenantId;

                        UserRegisteredIntegrationEvent userRegisteredIntegrationEvent = new UserRegisteredIntegrationEvent(
                                evt.TenantId,
                                evt.UserId,
                                evt.Username,
                                evt.Name,
                                evt.EmailAddress
                    );
                        userRegisteredIntegrationEvent.TimeStamp = evt.TimeStamp;
                        userRegisteredIntegrationEvent.Version = evt.Version;

                        //eventStore.Save(new List<IEvent> { userRegisteredIntegrationEvent });
                        _identityAccessIntegrationEventService.PublishThroughEventBusAsync(userRegisteredIntegrationEvent);
                        //return;
                    }

                    else if (domainEvent is UserEnablementChanged)
                    {
                        Console.WriteLine("To Do: UserEnablementChanged.");
                        //return;
                    }

                    else if (domainEvent is PersonNameChanged)
                    {
                        Console.WriteLine("To Do: PersonNameChanged.");
                        //return;
                    }

                    else if (domainEvent is PersonContactInformationChanged)
                    {
                        Console.WriteLine("To Do: PersonContactInformationChanged.");
                        //return;
                    }


                    //this.eventStore.Append(domainEvent);
                });

        }
    }
}
