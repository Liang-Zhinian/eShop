using DomainModels = SaaSEqt.IdentityAccess.Domain.Identity.Entities;
using SaaSEqt.IdentityAccess.Domain.Identity.Repositories;
using SaaSEqt.IdentityAccess.Infra.Data.Context;
//using ReadModels = SaaSEqt.IdentityAccess.Infra.Data.Models;
using System;
using System.Linq;

namespace SaaSEqt.IdentityAccess.Infra.Data.Repositories
{


	/// <summary>
	/// Contract for a collection-oriented repository of <see cref="Group"/> entities.
	/// </summary>
	/// <remarks>
	/// Because this is a collection-oriented repository, the <see cref="Add"/>
	/// method needs to be called no more than once per stored entity.
	/// Subsequent changes to any stored <see cref="Group"/> are implicitly
	/// persisted, and adding the same entity a second time will have no effect.
	/// </remarks>
	
    public class GroupRepository : DomainRepository<DomainModels.Group>, IGroupRepository
    {
        public GroupRepository(IdentityAccessDbContext context) : base(context) { }

		/// <summary>
		/// Stores a given <see cref="Group"/> in the repository.
		/// </summary>
		/// <param name="group">
		/// The instance of <see cref="Group"/> to store.
		/// </param>
		/// <remarks>
		/// Because this is a collection-oriented repository, the <see cref="Add"/>
		/// method needs to be called no more than once per stored entity.
		/// Subsequent changes to any stored <see cref="Group"/> are implicitly
		/// persisted, and adding the same entity a second time will have no effect.
		/// </remarks>
        public void Add(DomainModels.Group group){
            //ReadModels.Group g = new ReadModels.Group
            //{
            //    TenantId = Guid.Parse(group.TenantId.Id),
            //    Id = Guid.NewGuid(),
            //    Name = group.Name,
            //    Description = group.Description,
            //};
            base.Add(group);
            //base.SaveChanges();
        }

		/// <summary>
		/// Retrieves a <see cref="Group"/> from the repository,
		/// and implicitly persists any changes to the retrieved entity.
		/// </summary>
		/// <param name="tenantId">
		/// The identifier of a <see cref="Tenant"/> to which
		/// a <see cref="Group"/> may belong, corresponding
		/// to its <see cref="Group.TenantId"/>.
		/// </param>
		/// <param name="groupName">
		/// The name of a <see cref="Group"/>, matching
		/// the value of its <see cref="Group.Name"/>.
		/// </param>
		/// <returns>
		/// The instance of <see cref="Group"/> retrieved,
		/// or a null reference if no matching entity exists
		/// in the repository.
		/// </returns>
        DomainModels.Group IGroupRepository.GroupNamed(Guid tenantId, string groupName)
        {
            DomainModels.Group group = Find(_ => _.TenantId.Equals(tenantId)
                                          && _.Name.Equals(groupName)).First();

            return group; //new DomainModels.Group(tenantId, groupName, group.Description);
        }
    }
}