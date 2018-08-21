using System;
using System.Linq;
using SaaSEqt.IdentityAccess.Domain.Identity.Entities;
using SaaSEqt.IdentityAccess.Domain.Identity.Repositories;
using SaaSEqt.IdentityAccess.Infra.Data.Context;
//using ReadModels = SaaSEqt.IdentityAccess.Infra.Data.Models;

namespace SaaSEqt.IdentityAccess.Infra.Data.Repositories
{
    public class TenantRepository : DomainRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(IdentityAccessDbContext context):base(context){}

        public void Add(Tenant tenant)
        {
            //ReadModels.Tenant t = new ReadModels.Tenant();
            //t.Id = Guid.Parse(tenant.TenantId.Id);
            //t.Name = tenant.Name;
            //t.Description = tenant.Description;
            //t.Active = tenant.Active;
            base.Add(tenant);
            //base.SaveChanges();
        }

        public Tenant Get(Guid tenantId)
        {
            var tenant = this.Find(tenantId);
            return tenant;
            //return new DomainModels.Tenant(
            //    new DomainModels.TenantId(tenant.Id.ToString()),
            //    tenant.Name, 
            //    tenant.Description, 
            //    tenant.Active
            //);
        }

        public Tenant GetByName(string name)
        {
            var tenant = this.Find(_=>_.Name.Equals(name)).First();
            return tenant;
            //return new DomainModels.Tenant(
            //    new DomainModels.TenantId(tenant.Id.ToString()),
            //    tenant.Name,
            //    tenant.Description,
            //    tenant.Active
            //);
        }

        public Guid GetNextIdentity()
        {
            return Guid.NewGuid();
        }

        public void Register(Tenant tenant)
        {
            base.Add(tenant);
            //base.SaveChanges();
        }

        public void Remove(Tenant tenant)
        {
            //var t = this.Find(Guid.Parse(tenant.TenantId.Id));
            //tenant.Deactivate();
            base.Update(tenant);
            //base.SaveChanges();
        }
    }
}
