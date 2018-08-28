using SaaSEqt.IdentityAccess.Domain.Model.Access.Entities;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;
using SaaSEqt.IdentityAccess.Domain.Model.Access.Repositories;
using SaaSEqt.IdentityAccess.Infrastructure.Context;
//using ReadModels = SaaSEqt.IdentityAccess.Infrastructure.Models;
using System;
using System.Linq;

namespace SaaSEqt.IdentityAccess.Infrastructure.Repositories
{

    /// <summary>
    /// Contract for a collection-oriented repository of <see cref="Role"/> entities.
    /// </summary>
    /// <remarks>
    /// Because this is a collection-oriented repository, the <see cref="Add"/>
    /// method needs to be called no more than once per stored entity.
    /// Subsequent changes to any stored <see cref="Role"/> are implicitly
    /// persisted, and adding the same entity a second time will have no effect.
    /// </remarks>

    public class RoleRepository : DomainRepository<Role>, IRoleRepository
    {
        public RoleRepository(IdentityAccessDbContext context) : base(context) { }

        public void Add(Role role)
        {
            //ReadModels.Role r = new ReadModels.Role
            //{
            //    Id = Guid.NewGuid(),
            //    Description = role.Description,
            //    Name = role.Name,
            //    SupportsNesting = role.SupportsNesting,
            //    TenantId = Guid.Parse(role.TenantId.Id)
            //};
            base.Add(role);
            //base.SaveChanges();
        }

        public Role RoleNamed(Guid tenantId, string roleName)
        {
            Role role = Find(_ => _.TenantId.Equals(tenantId)
                        && _.Name.Equals(roleName)).First();

            return role; //new DomainModels.Role(tenantId, roleName, role.Description, role.SupportsNesting);
        }
    }
}