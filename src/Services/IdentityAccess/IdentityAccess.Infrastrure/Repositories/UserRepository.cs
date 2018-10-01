
using DomainModels = SaaSEqt.IdentityAccess.Domain.Model.Identity.Entities;
using SaaSEqt.IdentityAccess.Domain.Model.Identity.Repositories;
using SaaSEqt.IdentityAccess.Infrastructure.Context;
//using ReadModels = SaaSEqt.IdentityAccess.Infrastructure.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SaaSEqt.IdentityAccess.Infrastructure.Repositories
{

    /// <summary>
    /// Contract for a collection-oriented repository of <see cref="User"/> entities.
    /// </summary>
    /// <remarks>
    /// Because this is a collection-oriented repository, the <see cref="Add"/>
    /// method needs to be called no more than once per stored entity.
    /// Subsequent changes to any stored <see cref="User"/> are implicitly
    /// persisted, and adding the same entity a second time will have no effect.
    /// </remarks>

    public class UserRepository : DomainRepository<DomainModels.User>, IUserRepository
    {
        public UserRepository(IdentityAccessDbContext context) : base(context) { }

        public void Add(DomainModels.User user)
        {
            //ReadModels.User u = new ReadModels.User
            //{
            //    Id = Guid.NewGuid(),
            //    Username = user.Username,
            //    Password = user.Password,
            //    TenantId = Guid.Parse(user.TenantId.Id),
            //    IsEnabled = user.IsEnabled,
            //    Enablement = new Enablement(user.Enablement.Enabled,
            //                              user.Enablement.StartDate,
            //                              user.Enablement.EndDate),

            //    PersonalInfo = new PersonalInfo()
            //    {
            //        Email = user.Person.ContactInformation.EmailAddress.Address,
            //        FirstName = user.Person.Name.FirstName,
            //        LastName = user.Person.Name.LastName
            //    },

            //    PostalAddress = new PostalAddress()
            //    {
            //        StreetAddress = user.Person.ContactInformation.PostalAddress.StreetAddress,
            //        StreetAddress2 = "",
            //        City = user.Person.ContactInformation.PostalAddress.City,
            //        StateProvince = user.Person.ContactInformation.PostalAddress.StateProvince,
            //        CountryCode = user.Person.ContactInformation.PostalAddress.CountryCode,
            //        PostalCode = user.Person.ContactInformation.PostalAddress.PostalCode
            //    },

            //    PrimaryTelephone = user.Person.ContactInformation.PrimaryTelephone.Number,
            //    SecondaryTelephone = user.Person.ContactInformation.SecondaryTelephone.Number
            //};

            base.Add(user);
            //base.SaveChanges();
        }

        public DomainModels.User UserFromAuthenticCredentials(Guid tenantId, string username, string encryptedPassword)
        {
            DomainModels.User user = this.Find(_ => _.TenantId.Equals(tenantId)
                                                  && _.Username.Equals(username)
                                                    && _.Password.Equals(encryptedPassword)
                                              )
                .First();
            return user;

            //DomainModels.TenantId tid = new DomainModels.TenantId(user.TenantId.ToString());
            //DomainModels.Enablement enablement = new DomainModels.Enablement(user.Enablement.Enabled,
            //                              user.Enablement.StartDate,
            //                                                                 user.Enablement.EndDate);
            //DomainModels.FullName fullname = new DomainModels.FullName(user.PersonalInfo.FirstName, user.PersonalInfo.LastName);
            //DomainModels.EmailAddress email = new DomainModels.EmailAddress(user.PersonalInfo.Email);
            //DomainModels.PostalAddress postalAddress = new DomainModels.PostalAddress(
            //    user.PostalAddress.StreetAddress, user.PostalAddress.City, user.PostalAddress.StateProvince,
            //    user.PostalAddress.PostalCode, user.PostalAddress.CountryCode
            //);
            //DomainModels.Telephone primTel = new DomainModels.Telephone(user.PrimaryTelephone);
            //DomainModels.Telephone secTel = new DomainModels.Telephone(user.SecondaryTelephone);
            //DomainModels.ContactInformation ci = new DomainModels.ContactInformation(email, postalAddress,
            //                                                                         primTel, secTel);
            //DomainModels.Person person = new DomainModels.Person(
            //        tid,
            //        fullname,
            //    ci);

            //return new DomainModels.User(
                //tid,
                //user.Username,
                //user.Password,
                //enablement,
                //person);

        }

        public DomainModels.User UserWithUsername(Guid tenantId, string username)
        {
            var root = this.Find(y => y.Username.Equals(username))
                           .Include(y => y.Person)
                                .ThenInclude(p => p.Name)
                           .Include(y=>y.Person)
                               .ThenInclude(p=>p.ContactInformation)
                                    .ThenInclude(c=>c.EmailAddress)
                           .Include(y => y.Person)
                                .ThenInclude(p => p.ContactInformation)
                                    .ThenInclude(c => c.PostalAddress)
                           .Include(y => y.Person)
                               .ThenInclude(p => p.ContactInformation)
                                    .ThenInclude(c => c.PrimaryTelephone)
                           .Include(y => y.Person)
                               .ThenInclude(p => p.ContactInformation)
                                    .ThenInclude(c => c.SecondaryTelephone)
                           //.Include(y=>y.UserDescriptor)
                           .Include(y => y.Tenant)
                           .Include(y => y.Enablement);
                           
            DomainModels.User user = root.Where(_ => _.TenantId.Equals(tenantId))
                       .First();
            return user;
            
            //DomainModels.Enablement enablement = new DomainModels.Enablement(user.Enablement.Enabled,
            //                              user.Enablement.StartDate,
            //                                                                 user.Enablement.EndDate);
            //DomainModels.FullName fullname = new DomainModels.FullName(user.PersonalInfo.FirstName, user.PersonalInfo.LastName);
            //DomainModels.EmailAddress email = new DomainModels.EmailAddress(user.PersonalInfo.Email);
            //DomainModels.PostalAddress postalAddress = new DomainModels.PostalAddress(
            //    user.PostalAddress.StreetAddress, user.PostalAddress.City, user.PostalAddress.StateProvince,
            //    user.PostalAddress.PostalCode, user.PostalAddress.CountryCode
            //);
            //DomainModels.Telephone primTel = new DomainModels.Telephone(user.PrimaryTelephone);
            //DomainModels.Telephone secTel = new DomainModels.Telephone(user.SecondaryTelephone);
            //DomainModels.ContactInformation contactInformation = new DomainModels.ContactInformation(email, postalAddress,
            //                                                                         primTel, secTel);
            //DomainModels.Person person = new DomainModels.Person(
            //    tenantId,
            //        fullname,
            //    contactInformation);

            //return new DomainModels.User(
                //tenantId,
                //user.Username,
                //user.Password,
                //enablement,
                //person);
        }
    }
}