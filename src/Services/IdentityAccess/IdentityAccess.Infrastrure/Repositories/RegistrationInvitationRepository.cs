using System;
using System.Linq;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Repositories;
using SaaSEqt.IdentityAccess.Infrastructure.Context;
//using ReadModels = SaaSEqt.IdentityAccess.Infrastructure.Models;

namespace SaaSEqt.IdentityAccess.Infrastructure.Repositories
{
    public class RegistrationInvitationRepository : DomainRepository<RegistrationInvitation>, IRegistrationInvitationRepository
    {
        public RegistrationInvitationRepository(IdentityAccessDbContext context):base(context){}

        public void Add(RegistrationInvitation registrationInvitation)
        {
            //ReadModels.Tenant t = new ReadModels.Tenant();
            //t.Id = Guid.Parse(tenant.TenantId.Id);
            //t.Name = tenant.Name;
            //t.Description = tenant.Description;
            //t.Active = tenant.Active;
            base.Add(registrationInvitation);
            //base.SaveChanges();
        }

        public IQueryable<RegistrationInvitation> GetByTenantId(Guid tenantId)
        {
            var registrationInvitations = this.Find(_ => _.TenantId.Equals(tenantId));
            return registrationInvitations;
        }

        public RegistrationInvitation GetByDescription(string description)
        {
            var registrationInvitation = this.Find(_=>_.Description.Equals(description)).First();
            return registrationInvitation;
        }

        public Guid GetNextIdentity()
        {
            return Guid.NewGuid();
        }

        public void Remove(RegistrationInvitation registrationInvitation)
        {
            //var t = this.Find(Guid.Parse(tenant.TenantId.Id));
            //tenant.Deactivate();
            base.Update(registrationInvitation);
            //base.SaveChanges();
        }
    }
}
